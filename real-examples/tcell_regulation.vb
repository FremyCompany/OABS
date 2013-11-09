::::::::::::::::::::::::::::::::::::::::::
::: Create an alias for the [count] unit
::::::::::::::::::::::::::::::::::::::::::
[cell]=
	1.0	       [count]   

::::::::::::::::::::::::::::::::::::::::::
::: Define some constants
::::::::::::::::::::::::::::::::::::::::::
dt=
	0.25       [h]

μN=
	1.0...-3   [h^-1]
	
λN=
	1.0...-3   [h^-1 cell^-1]
	
...

::::::::::::::::::::::::::::::::::::::::::
::: Define the experiment behaviors
::::::::::::::::::::::::::::::::::::::::::
new TCellSet=
	new Set(
		
		N=
			count some TCell(Function = Naive)
		E=
			count some TCell(Function = Effective)
		M=
			count some TCell(Function = Memory)
		R=
			count some TCell(Function = Regulator)
			
		TOT=
			N + E + M + R
			
		CD4=
			N + E + M
		CD8=
			R
			
		CD4Ratio=
			CD4 / TOT
		CD8Ratio=
			CD8 / TOT
			
	)
	
	::::::::::::::::::::::::::::::::::::::::::
	::: Describe a T cell and its behavior
	::::::::::::::::::::::::::::::::::::::::::
	new TCell=
		new State(
			
			Function=
				some TCellFunction
			
			Age=
				some TCellAge
				
			DeathRate=
				Function.DeathRate
			
			ProliferationRate=
				Function.ProliferationRate
				
		)
			
		::::::::::::::::::::::::::::::::::::::::::
		new TCellFunction=
			new State(
				
				DeathRate=
					some number
				
				ProliferationRate=
					some number
			)
			
			::::::::::::::::::::::::::::::::::::::::::
			Naive=new(DeathRate= μN * dt, ProliferationRate= λN * TCellSet.E * dt)
			Effective=new(...)
			Memory=new(...)
			Regulator=new(...)
			
			::::::::::::::::::::::::::::::::::::::::::
			NaiveDifferentiation=
				new Transition(
					From= Naive
					To=   Effective
					Rate= ...
				)
		 
		::::::::::::::::::::::::::::::::::::::::::
		new TCellAge=
			new State(Next= some TCellAge)

			D3=new(Next= D3)
			D2=new(Next= D3)
			D1=new(Next= D2)
			D0=new(Next= D1)

	::::::::::::::::::::::::::::::::::::::::::
	::: Transitions chaning the amount of cells
	::::::::::::::::::::::::::::::::::::::::::
	TCellDeath=
		new Reaction(
			From=
				[ some TCell() as x ]
			To=
				[]
			Rate=
				x.DeathRate
		)

	TCellProliferation=
		new Reaction(
			From= 
				[ some TCell() as x ]
			To=
				[ 2 new [x](Age = x.Age.Next) ]
			Rate=
				x.ProliferationRate
		)
				
::::::::::::::::::::::::::::::::::::::::::
::: run some experiment
::::::::::::::::::::::::::::::::::::::::::
Run =
	new Experiment(
		Data=
			new TCellSet(
				States=
					[
						100 new TCell(Function=Naive,Age=D0)
						10  new TCell(Function=Regulator,Age=D0)
					]
			)
		Iterations=
			1[month] / dt
		OutputColumns=
			"TOT,N,E,M,R,CD4Ratio"
		OutputFile=
			"data.csv"
	)