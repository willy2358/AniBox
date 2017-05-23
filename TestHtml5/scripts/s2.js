function draw(canvasId){
	alert("oooo1111");
	var canvas = document.getElementById(canvasId);
	alert("oooo");
	if (null == canvas){
		return false;
	}
	alert("heellll");
	var context = canvas.getContext("2d");
	context.fillStyle = "#EEEEFF";
	context.fillRect(0, 0, 400, 300);
	context.fillStyle = "grey";
	context.strokeStyle = "blue";
	context.lineWidth = 1;
	context.fillRect(50, 50, 100, 100);
	context.strokeRect(50, 50, 100, 100);

};