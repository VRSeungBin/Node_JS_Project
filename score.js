const express = require('express');
const app = express();

let users = [];

app.use(express.json());

app.get('/', (req, res)=>{
    res.send('hello world');
});

app.get('/scores/:id', (req, res)=>{
    console.log('id :' + req.params.id);
    let user = users.find(x=>x.id == req.params.id);

    if(user === undefined)
    {
        res.send
        ({
            cmd : 1103, message : '잘못된 id 입니다.'
        });
    }
    else
    {
        res.send
        ({
            cmd : 1102, message : '', result : user
        });
    }

app.get('/scores/top3', (req, res)=>{
    let result = users.sort(function (a,b){
        return b.score - a.score;});
    
    result = result.slice(0,3);

    res.send({cmd : 1101, message : '', result});
    });

});

app.post('/scores', (req, res)=>{
    const {id, score} = req.body;

    console.log(req.body);

    let result = {
        cmd : -1, message : ''
    };

    let user = users.find(x=>x.id==id);
    
    if(user === underfined){
        users.push({id, score});
        result.cmd = 1001;
        result.message = '점수가 신규 등록되었습니다.'
    }
    else{
        console.log(score, id , user.score);
        
        if(score > user.score){
            user.score = score;
            result.cmd = 1002;
            result.message = '점수가 갱신 되었습니다.';
        }
        else{
            result.cmd = 1003;
        }
    }
    console.log(users);
    res.send(result);
});

app.listen(3030, ()=> {
    console.log('server is running at 3030 port');
})