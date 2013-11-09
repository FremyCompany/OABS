::::::::::::::::::::::::::::::::::::::::::::::::
::: Handles the level of IL2
::::::::::::::::::::::::::::::::::::::::::::::::

new IL2Holder=
	new Set(
		NumberOfIL2=
			count some IL2
		IL2ToCreate= 
			some number
		IL2ToConsume=
			some number
	)
	new IL2=
		new State()
		Generate=
			new Reaction(
				From= []
				To=   [ IL2Holder.IL2ToCreate new IL2 ]
			)
		Consume=
			new Reaction(
				From= [some IL2]
				To=   []
				Rate= IL2Holder.IL2ToConsume / IL2Holder.NumberOfIL2
			)

::::::::::::::::::::::::::::::::::::::::::::::::
::: Handles a population of cell
::::::::::::::::::::::::::::::::::::::::::::::::

new CellHolder=
	new Set(
		NumberOfCells=
			count some Cell
		CellCloneRate=
			some number
		CellDieRate=
			some number
	)
	new Cell=
		new State()
		Clone=
			new Reaction(
				To=   [ 2 x ]
				Rate= CellHolder.CellCloneRate
			)
		Die=
			new Reaction(
				To=   []
				Rate= CellHolder.CellDieRate
			)

::::::::::::::::::::::::::::::::::::::::::::::::
::: In this experiment, we try to find out 
::: whether the IL2 level or the cell reproduction  
::: rate are limitative by linking the level of
::: IL2 and of cells using formulas
::::::::::::::::::::::::::::::::::::::::::::::::

new CellBox=
	new Set()
	new IL2Holder(
		IL2GenerationRate= 
			(0.25 * NumberOfCells) / (NumberOfIL2)
		IL2ConsumptionRate=
			(CellCloneRate * NumberOfCells) / (NumberOfIL2)
	)
	new CellHolder(
		CellCloneRate=
			Min(0.5 * NumberOfIL2 / NumberOfCells, 0.5)
		CellDieRate=
			0.1
	)

Run=
	new Experiment(
		Data = 
			new CellBox(
				States = [ 100 new Cell() ]
			)
		Iterations=
			1000
		OutputFile=
			"data.csv"
	)