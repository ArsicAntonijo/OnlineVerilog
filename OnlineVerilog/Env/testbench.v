module testbench;
  reg a;
  wire y;

  //Design Instance
  topmodule jinv(y,a);
  
	initial
	begin
		$display ("RESULT\ta\s-\sy");

		a = 1; # 100; // Another value
		if ( y == 0 ) // Test for inversion
			$display ("  PASS  \t%d - %d",a,y);
		else
			$display ("  FAIL \t%d - %d",a,y);

		a = 0; # 100; // Initial value is set
		if ( y == 1 ) // Test for inversion
			$display ("  PASS  \t%d - %d",a,y);
		else
			$display ("  FAIL  \t%d - %d",a,y);

		a = 1; # 50; // Another value
		if ( y == 0 ) // Test for inversion
			$display ("  PASS  \t%d - %d",a,y);
		else
			$display ("  FAIL  \t%d - %d",a,y);

		a = 0; # 100; // Initial value is set
		if ( y == 1 ) // Test for inversion
			$display ("  PASS  \t%d - %d",a,y);
		else
			$display ("  FAIL  \t%d - %d",a,y);

	end
	  
  //enabling the wave dump
  initial begin 
    $dumpfile("dump.vcd"); $dumpvars;
  end
  
endmodule