function isInt(n) {	
	var Int = parseInt(n,10);	
	if(isNaN(Int)) return false;
	if(Int.toString() != n) return false;	
	return true;
}
