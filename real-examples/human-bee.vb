new World=
	new Set()
	
	::::::::::::::::::::::::::::::::::::::::::::::::
	new LocatedObject=
		new State(
			X = some number
			Y = some number
		)
	
	new MovingObject=
		new LocatedObject(
			Speed = some number
		)
	
	new Human=
		new MovingObject(
			IsSick = some boolean
			Speed = if IsSick then 0 else 1
		)
	new Bee=
		new MovingObject(
			Speed = 2
		)
	
	::::::::::::::::::::::::::::::::::::::::::::::::
	ObjectMoves=
		new Reaction(
			From= [ some MovingObject as o ]
			To=   [ new[ox](  X= o.X + SRand()*o.Speed, Y= o.Y + SRand()*o.Speed) ]
			Rate= 0.9
		)
	
	BeeAttacksHuman=
		new Reaction(
			From=
				[
					some Human(IsSick=False, X as HX, Y as HY) as x
					some Bee(X as BX, Y as BY) as y
					where Abs(HX-BX) <= 1
					where Abs(HY-BY) <= 1
				]
			To=
				[
					1 new[x](IsSick=True)  ::: THE HUMAN GETS SICK, THE BEE GETS KILLED
				]
			Rate=
				0.10 / (1 + Abs(HX-BX) + Abs(HY-BY))
		)
	
	