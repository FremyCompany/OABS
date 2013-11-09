::::::::::::::::::::::::::::::::::::::::::
new SetOfCell=
	new Set()
	
	::::::::::::::::::::::::::::::::::::::::::
	new Cell=
		new State()
		new AttackDefense()

::::::::::::::::::::::::::::::::::::::::::
new AttackDefense=
	(
		
		DefenseState=
			some DefenseState
			default Protected
			
		AttackState=
			some AttackState
			default NotAttacked
		
	)

	::::::::::::::::::::::::::::::::::::::::::
	new DefenseState=
		new State()

		::::::::::::::::::::::::::::::::::::::::::
		NotProtected=new()
		Protected=new()
		
		::::::::::::::::::::::::::::::::::::::::::
		Unprotect=
			new Transition(
				To=           NotProtected
				ConflictRate= if x = NotProtected then 0.66 else 0.33
			)

		Reprotect=
			new Transition(
				To=           Protected
				ConflictRate= if x = Protected then 0.66 else 0.33
			)

	::::::::::::::::::::::::::::::::::::::::::
	new AttackState=
		new State()

		::::::::::::::::::::::::::::::::::::::::::
		NotAttacked=new()
		Attacked=new()
		
		::::::::::::::::::::::::::::::::::::::::::
		Unattack=
			new Transition(
				To=           NotAttacked
				ConflictRate= if x = NotAttacked then 2.0 else 1.0
			)

		Reattack=
			new Transition(
				To=           Attacked
				ConflictRate= if x = Atacked then 2.0 else 1.0
			)
	
	::::::::::::::::::::::::::::::::::::::::::
	DestructionByAttack=
		new Reaction(
			From=   [ some AttackDefense(DefenseState=NotProtected, AttackState=Attacked) ]
			To=     []
			Rate=   0.9
		)

	ProliferationByDefense=
		new Reaction(
			From=
				[
					some AttackDefense(DefenseState=Protected, AttackState=NotAttacked)
				]
			To=
				[
					1 new[x]()
					1 new[x](DefenseState=NotProtected)
				]
			Rate=
				0.5
		)
