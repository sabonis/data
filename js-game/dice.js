var Dice = function(name) {
    this.value = Math.floor(Math.random()*6+1);
    this.dice_n_name = name;
    this.isStop = false;
}

Dice.prototype.spin = function(diceId, onSpinnedLis) {
    var classNames= ["1", "t", "2", "s", "3", "e", "4", "t", "5", "s", "6", "e"];
    var count = 0;
    var i = setInterval(function(){
        this.setResultOnView(classNames[count++]);
        if (count === 12) {
            count = 0
            if (onSpinnedLis)
                onSpinnedLis();
        }
        if (this.isStop) {
            this.setResultOnView(this.value);
            clearInterval(i);
        }
    }.bind(this), 250);
};

Dice.prototype.setResultOnView = function(name) {
    var dice_n_name = this.dice_n_name;
    var dice = document.getElementById(dice_n_name);
    dice.className = "dice_" + name;
}

Dice.prototype.stop = function() {
    this.isStop = true;
}
Dice.prototype.reset = function() {
    this.isStop = false;
    this.value = Math.floor(Math.random()*6+1);
}

var Dices = function(num) {
    this.isStarted = false;
    this.dices = []
    for (var i = 0; i < num; i++) {
        this.dices[i] = new Dice('dice' + (i + 1));
    }

    this.startSpin = function () {
        this.isStarted = true;
        this.dices.forEach(function(v, k) {
            v.spin()
        })
        var count = 0;
        var i = setInterval(function() {
            this.dices[count++].stop();
            if (count === this.dices.length) {
                clearInterval(i);
                setTimeout(function() {
                    this.reset();
                }.bind(this), 300);
            }
        }.bind(this), 1000);
    };

    this.reset = function() {
        this.isStarted = false;
        this.dices.forEach(function(v, k) {
            v.reset();
        })
    }
}
