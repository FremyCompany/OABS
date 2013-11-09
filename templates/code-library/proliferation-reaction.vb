::::::::::::::::::::::::::::::::::::::::::
::: TEMPLATE
::::::::::::::::::::::::::::::::::::::::::
new LogisticProliferation=
	new Reaction(
		CurrentPopulation=
			some number
		Capacity=
			some number
		InitialRate=
			some number
		Rate=
			InitialRate * (1 - CurrentPopulation/Capacity)
		To=
			[ 2 new[x] ]
	)

::::::::::::::::::::::::::::::::::::::::::
::: USAGE
::::::::::::::::::::::::::::::::::::::::::
new CellSet=
	new Set(TOT = count some Cell)
	new Cell=
		new State()
		Proliferation=
			new LogisticProliferation(
				CurrentPopulation=CellSet.TOT
				Capacity=1000
				InitialRate=0.05
			)
