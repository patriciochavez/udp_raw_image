var http = require('http');
var path = require('path');
var multer  = require('multer');
var storage = multer.memoryStorage();
var upload = multer({
  storage: storage
});
var express = require('express');
var fileBuffer = null;
var fileName = null;
var fileBase64 = null;

var app = express();
var httpServer = http.createServer(app).listen(8090);

app.post('/', upload.any(), function (req, res, next) {
  fileBuffer = req.files[0].buffer;
  fileName = req.files[0].originalname;
  fileBase64 = fileBuffer.toString('base64');
  return res.send({ok: true});  
});

/*app.get('/', function (req, res, next) {
  res.type('image/jpeg'); //for setting mime type
  //return res.end(fileBase64, 'base64');
  return res.end(fileBuffer);
});

app.get('/autorefresh.html', function (req, res, next) {
	res.sendFile( __dirname + '/autorefresh.html'); 
});
*/

app.get(/^(.+)$/, function(req, res){ 
    switch(req.params[0]) {
        case '/test':
            res.send("Ok!");
            break;
        case '/buffer':
             res.type('image/jpeg'); //for setting mime type  	     
	     return res.end(fileBuffer);
            break;            
    default: 
            res.sendFile( __dirname + req.params[0]); 
    }
 });


//app.listen(8080);

