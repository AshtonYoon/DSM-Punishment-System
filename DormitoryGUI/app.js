var express = require('express');
var app = express();
var request = require('request');
var mysql = require('mysql');
var bodyParser = require('body-parser');
var utf8 = require('utf8');
var server = app.listen(3141, function () {
    console.log("Express server has started on port 3141")
})

app.use(bodyParser.json());

var connection = mysql.createConnection({
    host: 'localhost',
    port: 3306,
    user: 'root',
    password: 'sotkfkddmsdjeldp',
    database: 'dormitory'
});

connection.connect(function (err) {
    if (err) {
        console.error('mysql connection error');
        console.error(err);
        throw err;
    }
    else {
        console.log('mysql connected');
    }
});

app.all('/', function (req, res) {
    console.log(req.path);
    res.send("Hello World!");

});

//TODO : 로그 저장 필요

//getTeacher 과 같은 함수
app.all('/login', function (req, res) {
    console.log(req.path);
    var id = req.body.ID;
    var pw = req.body.PW;
	var sql = 'SELECT TEACHER_DATA.TEACHER_UUID TEACHER_UUID, TEACHER_DATA.TEACHER_NAME TEACHER_NAME, ' 
	+ 'PERMISSION_DATA.STUDENT_MANAGE STUDNET_MANAGE, PERMISSION_DATA.SCORE_MANAGE SCORE_MANAGE FROM TEACHER_DATA JOIN PERMISSION_DATA ON PERMISSION_DATA.PERMISSION_TYPE = TEACHER_DATA.PERMISSION_TYPE WHERE TEACHER_DATA.ID = '
	+ mysql.escape(id) + ' AND TEACHER_DATA.PW = ' + mysql.escape(pw);
    res.header("Content-Type", "application/json; charset=utf-8");
    var query = connection.query(sql, function (err, rows) {
        if (err) {
            throw err;
        }
        res.json(rows[0]);
    });
});


/* add teacher */
app.all('/teacher/add', function (req, res) {
	res.header("Content-Type", "application/json; charset=utf-8");
	var dest = req.body.ID;
	var name = req.body.DATA.NAME;
	var id = req.body.DATA.ID;
	var pw = req.body.DATA.PW;
	var type = req.body.DATA.TYPE;
    console.log(req.path);
	var sql = 'if((SELECT PERMISSION_DATA.IS_ADMIN '
		+ 'FROM TEACHER_DATA '
		+ 'JOIN PERMISSION_DATA '
		+ 'ON PERMISSION_DATA.PERMISSION_TYPE = TEACHER_DATA.PERMISSION_TYPE '
		+ 'WHERE TEACHER_DATA.TEACHER_UUID = ' + mysql.escape(dest) + ') = 1, '
		+ 'INSERT INTO TEACHER_DATA (USER_NAME, PERMISSION_TYPE, ID, PW) '
		+ 'VALUES (\'' + mysql.escape(name) +  '\',\'' + mysql.escape(type) + '\',\''
		+ mysql.escape(id) + '\',\'' + mysql.escape(pw) + '\'),)';
    var query = connection.query(sql, function (err, rows) {
        if (err) {
            throw err;
        }
        console.log(rows);
        res.json(rows);
    });

});

app.all('/permission/set', function (req, res) {
	res.header("Content-Type", "application/json; charset=utf-8");
    console.log(req.path);
    console.log(req.body);
    var values = req.body;
    values.forEach(function (item) {
		var sql = "UPDATE PERMISSION_DATA SET SCORE_MANAGE = " 
				+ item.SCORE_MANAGE + " , STUDENT_MANAGE = " + item.STUDENT_MANAGE 
				+ " WHERE PERMISSION_TYPE = " + item.PERMISSION_TYPE;
        var query = connection.query(sql, function (err, rows) {
            if (err) {
                throw err;
            }
        });
    });
});

//getPermission
app.all('/permission/get', function (req, res) {
	res.header("Content-Type", "application/json; charset=utf-8");
    console.log(req.path);
    console.log(req.body);
	var sql = "select * from PERMISSION_DATA";
     var query = connection.query(sql, function (err, rows) {
        if (err) {
            throw err;
        }
        console.log(rows);
        res.json(rows);
    });
});

//giveScore
app.all('/score/give', function(req, res){
	console.log(req.path);
	console.log(req.body);
	var array = req.body.TARGET;
	console.log(array);
	var result = "";
	res.header("Content-Type", "application/json; charset=utf-8");
	array.forEach(function(value) {
		var str = 'insert into SCORE_LOG (TEACHER_UUID, STUDENT_UUID, POINT_UUID, POINT_VALUE) values (' 
		+ mysql.escape(req.body.DEST_UUID) + ', ' + mysql.escape(req.body.value) + ', ' + mysql.escape(req.body.POINT_UUID) + ', '
		+ ', ' + mysql.escape(req.body.POINT_VALUE) + ')';
		console.log(str);
		var query = connection.query(str , function (err, rows) {
		if(err){
			throw err;
		}
		console.log(rows);
		result += rows;
		});
	});
	res.json(result);
});

//getStudent
app.all('/student/get', function (req, res) {
    res.header("Content-Type", "application/json; charset=utf-8");
    console.log(req.path);
	var uuid = req.body.USER_UUID;
	var sql = 'select USER_UUID, USER_NAME, TOTAL_GOOD_SCORE GOOD_SCORE, TOTAL_BAD_SCORE BAD_SCORE, PUNISH_STATUS' 
			+ ' from STUDENT_DATA';
    var query = connection.query(sql, function (err, rows) {
        if (err) {
            throw err;
        }
        res.json(rows);
    });

});

//studentLog
app.all('/student/log', function (req, res) {
    console.log(req.path);
    res.header("Content-Type", "application/json; charset=utf-8");
	var sql = 'SELECT SCORE_LOG.SCORE_UUID SCORE_UUID, '
				+ 'RULE_DATA.POINT_MEMO POINT_MEMO, RULE_DATA.POINT_TYPE, '
				+ 'SCORE_LOG.POINT_VALUE POINT_VALUE, TEACHER_DATA.TEACHER_NAME TEACHER_NAME, '
				+ 'SCORE_LOG.CREATE_TIME '
				+ 'FROM SCORE_LOG '
				+ 'JOIN RULE_DATA '
				+ 'ON RULE_DATA.POINT_UUID = SCORE_LOG.POINT_UUID '
				+ 'JOIN TEACHER_DATA '
				+ 'ON TEACHER_DATA.TEACHER_UUID = SCORE_LOG.TEACHER_UUID '
				+ 'WHERE SCORE_LOG.STUDENT_UUID = ' + mysql.escape(req.body.USER_UUID);
    var query = connection.query(sql, function (err, rows) {
        if (err) {
            throw err;
        }
        res.json(rows);
    });

});

//editScore
app.all('/score/edit', function (req, res) {
    console.log(req.path);
	var teacher = req.body.TEACHER_UUID;
	var score_uuid = req.body.SCORE_UUID;
	var type = req.body.DATA.TYPE;
	var sql = "";
	switch(type){
		case 0 :{
			//devare score
			sql = "DEvarE FROM SCORE_LOG WHERE SCORE_UUID = " + mysql.escape(score_uuid);
			break;
		}
		case 1 :{
			//edit score
			sql = "UPDATE SCORE_LOG SET POINT_VALUE = " + mysql.escape(req.body.DATA.POINT) 
			+ " WHERE SCORE_UUID = " + mysql.escape(score_uuid);
			break;
		}
		case 2 :{
			//edit score and score rule(?)
			sql = "UPDATE SCORE_LOG SET POINT_VALUE = " + mysql.escape(req.body.DATA.POINT) 
			+ ", POINT_UUID = " + mysql.escape(req.body.DATA.POINT_UUID)
			+ " WHERE SCORE_UUID = " + mysql.escape(score_uuid);
			break;
		}
		default : break;
	}
	var query = connection.query(sql, function (err, rows) {
        if (err) {
            throw err;
        }
        res.json(rows);
    });
});

//getRule
app.all('/rule/get', function (req, res) {
    console.log(req.path);
    res.header("Content-Type", "application/json; charset=utf-8");
	var sql = "SELECT * FROM RULE_DATA";
    var query = connection.query(sql, function (err, rows) {
        if (err) {
            throw err;
        }
        res.json(rows);
    });

});

//addRule
//현재 상벌점 규칙 관리에 대한 제한 사항은 없는것 같아서 그냥 다 추가할수 있도록 해둠
app.all('/rule/add', function (req, res) {
    console.log(req.path);
    res.header("Content-Type", "application/json; charset=utf-8");
	var dest = req.body.DEST;
	var rule = req.body.RULE;
	var sql = "INSERT INTO RULE_DATA (POINT_TYPE, POINT_MEMO, POINT_MIN, POINT_MAX) VALUES ("
			+ mysql.escape(rule.POINT_TYPE) + ", \'" + mysql.escape(rule.POINT_MEMO)
			+ "\', " + mysql.escape(rule.POINT_MIN) + ", " + mysql.escape(rule.POINT_MAX) + ")";
    var query = connection.query(sql, function (err, rows) {
        if (err) {
            throw err;
        }
        res.json(rows);
    });

});

//delRule
app.all('/rule/del', function (req, res) {
    console.log(req.path);
    res.header("Content-Type", "application/json; charset=utf-8");
	var dest = req.body.DEST;
	var point_uuid = req.body.POINT_UUID;
	var sql = "devare from RULE_DATA where POINT_UUID = " + mysql.escape(point_uuid);
    var query = connection.query(sql, function (err, rows) {
        if (err) {
            throw err;
        }
        res.json(rows);
    });

});

//editRule
app.all('/rule/edit', function (req, res) {
    console.log(req.path);
    res.header("Content-Type", "application/json; charset=utf-8");
	var dest = req.body.DEST;
	var point_uuid = req.body.POINT_UUID;
	var point_max = req.body.POINT_MAX;
	var point_min = req.body.POINT_MIN;
	var point_memo = req.body.POINT_MEMO;
	var sql = "update RULE_DATA set POINT_MEMO = \'" + mysql.escape(point_memo) + "\', "
			+ "POINT_MIN = " + mysql.escape(point_min) + ", POINT_MAX = " + mysql.escape(point_max)
			+ "WHERE POINT_UUID = " + mysql.escape(point_uuid);
	var query = connection.query(sql, function (err, rows) {
        if (err) {
            throw err;
        }
        res.json(rows);
    });
});




//=====================================================
//					OLD SOURCE!!!
//=====================================================

// app.all('/student/add', function (req, res) {
    // console.log(req.path);

    // var sql = "INSERT INTO student_data SET ? ON DUPLICATE KEY UPDATE imestamp = VALUES(Timestamp)";

    // var values = req.body;
    // /*var values = [
        // { school_num: "30217", name: "정원태" },
        // { school_num: "30218", name: "정현태" }
    // ];*/

    // values.forEach(function (item) {
        // var query = connection.query(sql, item, function (err, rows) {
            // if (err) {
                // throw err;
            // }
            // res.json(rows);
        // });
    // });
// });

// app.all('/student/get', function (req, res) {
    // console.log(req.path);
    // res.header("Content-Type", "application/json; charset=utf-8");
    // var query = connection.query('select * from student_data order by student_data.user_school_number', function (err, rows) {
        // if (err) {
            // throw err;
        // }
        // console.log(rows);
        // res.json(rows);
    // });

// });

// app.all('/info/master', function (req, res) {
    // var query = connection.query('select * from student_data order by student_data.user_school_number', function (err, rows) {
            // if (err) {
                // throw err;
            // }
            // console.log(rows);
            // res.json(rows);
    // });
// });


// app.all('/info/detail', function (req, res) {
    // console.log(req.path);
    // res.header("Content-Type", "application/json; charset=utf-8");
    // var query = connection.query('select * from log_data '
	// + 'join score_data on score_data.point_uuid = log_data.point_uuid '
        // + 'where log_data.user_uuid =' + req.body.user_uuid, function (err, rows) {
        // if (err) {
            // throw err;
        // }
        // console.log(rows);
        // res.json(rows);
    // });

// });


// app.all('/student/point/add', function (req, res) {
    // console.log(req.path);
    // res.header("Content-Type", "application/json; charset=utf-8");

    // var TEACHER_UUID = req.body.TEACHER_UUID;
    // var STUDENT_UUID = req.body.STUDENT_UUID;
    // var POINT_TYPE = req.body.POINT_TYPE;
    // var POINT_VALUE = req.body.POINT_VALUE;
    // var POINT_UUID = req.body.POINT_UUID;
    // var LOG_MEMO = req.body.LOG_MEMO;



    // console.log('==================== Update Log =====================');
    // var sql = 'insert into log_data (user_uuid, point_uuid, point_value, log_memo, teacher_uuid) values ('
        // + '?, '
        // + mysql.escape(POINT_UUID) + ', '
        // + mysql.escape(POINT_VALUE) + ', '
        // + mysql.escape(LOG_MEMO) + ', '
        // + mysql.escape(TEACHER_UUID) + ');';

     // var logger = connection.query(sql, item, function (err, rows) {
            // if (err) {
                // throw err;
            // }
            // console.log(rows);
     // });
// });

// app.all('/scoreinfo/get', function(req, res){
	// console.log(req.path);
	// res.header("Content-Type", "application/json; charset=utf-8");
	// var query = connection.query('select * from score_data', function (err, rows) {
	// if(err){
		// throw err;
	// }
	// console.log(rows);
	// res.json(rows);
	// });
// });	

// app.all('/scoreinfo/add', function(req, res){
	// console.log(req.path);
	// console.log(req.body);
	// res.header("Content-Type", "application/json; charset=utf-8");
	// var query = connection.query('insert into score_data (POINT_TYPE, POINT_MEMO, POINT_VALUE_MIN, POINT_VALUE_MAX) values (' + mysql.escape(req.body.type) + ', ' + mysql.escape(req.body.memo) + ', ' + mysql.escape(req.body.min) + ', ' + mysql.escape(req.body.max) + ')', function (err, rows) {
	// if(err){
		// throw err;
	// }
	// });
// });

// app.all('/score/add', function(req, res){
	// console.log(req.path);
	// console.log(req.body);
	// var array = req.body.students;
	// console.log(array);
	// var result = "";
	// res.header("Content-Type", "application/json; charset=utf-8");
	// array.forEach(function(value) {
		// var str = 'insert into log_data (USER_UUID, POINT_UUID, POINT_VALUE, LOG_MEMO, TEACHER_UUID) values (' + mysql.escape(value) + ', ' + mysql.escape(req.body.uuid) + ', ' + mysql.escape(req.body.value) + ', ' + mysql.escape(req.body.memo) + ', ' + mysql.escape(req.body.teacher) + ')';
		// console.log(str);
		// var query = connection.query(str , function (err, rows) {
		// if(err){
			// throw err;
		// }
		// console.log(rows);
		// result += rows;
		// });
	// });
	// res.json(result);
// });

// app.all('/excel/get', function(req, res){
	// console.log(req.path);
	// res.header("Content-Type", "application/json; charset=utf-8");
	// var query = connection.query('select * from log_data join student_data on student_data.user_uuid = log_data.user_uuid join score_data on score_data.point_uuid = log_data.point_uuid order by student_data.user_school_room_number', function(err, rows){
		// console.log(rows);
		// res.json(rows);
	// });
// });
// /* abstract method */
// // ㅁㅁ 딱히 추가할게 없네.. 클라쪽에서 다 처리해주도록 하니까..

// app.all('/student/edit', function (req, res) {
    // console.log(req.path);
	// res.header("Content-Type", "application/json; charset=utf-8");
    // var values = req.body;
    // /*var values = [
        // { school_num: "30217", name: "정원태" },
        // { school_num: "30218", name: "정현태" }
    // ];*/

    // var query = connection.query('devare from student_data', function (err, rows) {
        // console.log(rows);
    // });

    // values.forEach(function (item) {
        // var query = connection.query("INSERT INTO student_data (user_name, user_school_number) VALUES (" + mysql.escape(item.name) + "," + mysql.escape(item.num) + ")", function (err, rows) {
            // if (err) {
                // throw err;
            // }
            // console.log(rows);
	 // });
    // });
	// res.send("{'status':'ok'}");
// });
