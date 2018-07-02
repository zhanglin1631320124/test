function Scroll() {
    this.num = 3; //当前容许多少个
    this.speed = 10; //速度(毫秒) 
    this.space = 2; //每次移动(px) 
    this.pageWidth = 132; //翻页宽度 
    this.fill = 0; //整体移位 
    this.moveLock = false;
    this.moveTimeObj = "MoveTimeObj";
    this.comp = 0;
    this.autoPlayObj = "AutoPlayObj";
    this.scrollList1;
    this.scrollList2;
    this.leftButton;
    this.rightButton;
    this.scrollAll;

    //构造函数
    this.Init = function() {
        if (this.GetObj(this.scrollList1).clientWidth > this.num * this.pageWidth) {
            this.GetObj(this.scrollList2).innerHTML = this.GetObj(this.scrollList1).innerHTML;
            this.GetObj(this.scrollAll).scrollLeft = this.fill;
            //this.AutoPlay();	
            var self = this;
            //this.GetObj(this.scrollAll).onmouseout = function(){self.AutoPlay();} 
            //this.GetObj(this.scrollAll).onmouseover = function(){clearInterval(this.autoPlayObj);} 
            this.GetObj(this.leftButton).onmouseover = function() { self.GoLeft(); }
            this.GetObj(this.leftButton).onmouseout = function() { self.StopLeft(); }
            this.GetObj(this.rightButton).onmouseover = function() { self.GoRight(); }
            this.GetObj(this.rightButton).onmouseout = function() { self.StopRight(); }
        }
    }
    //根据ID取对象
    this.GetObj = function(objName) {
        if (document.getElementById) {
            return eval('document.getElementById("' + objName + '")');
        }
        else {
            return eval('document.all.' + objName);
        }
    }
    //自动滚动 
    this.AutoPlay = function() {
        clearInterval(this.autoPlayObj);
        var self = this;
        this.autoPlayObj = setInterval(function() { self.GoLeft(); self.StopLeft(); }, 2000); //间隔时间 
    }
    //向右滚动
    this.GoRight = function() {
        if (this.moveLock) return;
        clearInterval(this.autoPlayObj);
        this.moveLock = true;
        var self = this;
        this.moveTimeObj = setInterval(function() { self.ScrRight(); }, this.speed);
    }
    //右滚停止
    this.StopRight = function() {
        clearInterval(this.moveTimeObj);
        if (this.GetObj(this.scrollAll).scrollLeft % this.pageWidth - this.fill != 0) {
            this.comp = this.fill - (this.GetObj(this.scrollAll).scrollLeft % this.pageWidth);
            this.CompScr();
        }
        else {
            this.moveLock = false;
        }
        //this.AutoPlay(); 
    }
    //右滚动作 
    this.ScrRight = function() {
        if (this.GetObj(this.scrollAll).scrollLeft <= 0) {
            this.GetObj(this.scrollAll).scrollLeft = this.GetObj(this.scrollAll).scrollLeft + this.GetObj(this.scrollList1).offsetWidth
        }
        this.GetObj(this.scrollAll).scrollLeft -= this.space;
    }
    //向左滚动
    this.GoLeft = function() {
      
        clearInterval(this.moveTimeObj);
        if (this.moveLock) return;
        clearInterval(this.autoPlayObj);
        this.moveLock = true;
        this.ScrLeft();
        var self = this;
        this.moveTimeObj = setInterval(function() { self.ScrLeft(); }, this.speed);
    }
    //左滚停止 
    this.StopLeft = function() {
        clearInterval(this.moveTimeObj);
        if (this.GetObj(this.scrollAll).scrollLeft % this.pageWidth - this.fill != 0) {
            this.comp = this.pageWidth - this.GetObj(this.scrollAll).scrollLeft % this.pageWidth + this.fill;
            this.CompScr();
        }
        else {
            this.moveLock = false;
        }
        //this.AutoPlay(); 
    }
    //左滚动作 
    this.ScrLeft = function() {
        if (this.GetObj(this.scrollAll).scrollLeft >= this.GetObj(this.scrollList1).scrollWidth) {
            this.GetObj(this.scrollAll).scrollLeft = this.GetObj(this.scrollAll).scrollLeft - this.GetObj(this.scrollList1).scrollWidth;
        }
        this.GetObj(this.scrollAll).scrollLeft += this.space;
    }

    this.CompScr = function() {
        var num;
        if (this.comp == 0) {
            this.moveLock = false;
            return;
        }
        if (this.comp < 0) { //右滚 
            if (this.comp < -this.space) {
                this.comp += this.space;
                num = this.space;
            }
            else {
                num = -this.comp;
                this.comp = 0;
            }
            this.GetObj(this.scrollAll).scrollLeft -= num;
            var self = this;
            setTimeout(function() { self.CompScr(); }, this.speed);
        }
        else { //左滚 
            if (this.comp > this.space) {
                this.comp -= this.space;
                num = this.space;
            }
            else {
                num = this.comp;
                this.comp = 0;
            }
            this.GetObj(this.scrollAll).scrollLeft += num;
            var self = this;
            setTimeout(function() { self.CompScr(); }, this.speed);
        }
    }
}