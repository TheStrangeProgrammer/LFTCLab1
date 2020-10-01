{
	let i,nr,isPrime;
	nr=0;
	isPrime=true;
	input(nr);
	if(nr < 2){
		isPrime=false;
	}
	elseif(nr==2){
		isPrime=true;
	}
	elseif(nr%2){
		isPrime=false;
	}
	else{
		for(i=0;i*i<nr;i++){
			if (nr % i == 0){
				isPrime=false;
			}
		}
	}
	if(isPrime){
		output("prime");
	}
	else{
		output("not prime");
	}
}