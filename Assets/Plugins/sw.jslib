mergeInto(LibraryManager.library, {
  ReturnScore: function(x) {
    window.top.postMessage("GameOver:" + x);
    console.log(x);
    return x;
  },

  SendGameStart: function() {
    window.top.postMessage("gameStart");
    console.log("STARTED");
  },
});