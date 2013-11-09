Optimizable Agent-Based Simulations=====================================## IntroductionThe goal of this language is to define a human-editable description format for dynamic simulations based on statecharts and transitions.It is particularly intended for biologists and chemists who need to describe systems at the _agent level_, want _high-performance_ simulations but do neither want to be limited by the possibilities of the simulation nor to deal with low-level code themselves. Given those principles, it is obviously inspired (but different) from SBML. In the following chapters we will try to explain why those differences actually make it better.## Similarities with SBMLLike in SBML, objects live in _sets_ (the equivalent of compartments). Sets contain different _states_ in various amounts, and whose populations can evolve over time. Like in SBML, reactions can transform a group of _producers states_ into a group of _produced states_. Finally, the _rate_ at which those reactions occur can be defined using arbitrary mathematical expressions (even if those can be written as normal formulas, and do not have to be converted to MathML like in SBML).## Differences with SBMLUnlike SBML, OABS has the notion of _substates_. Example: a MilitaryUnit can either be (WithHorse / WithoutHorse), (WithBow / WithSword), (New / Experienced / VeryExperienced). Representing such combinations without substates would required `2*2*3` = 12 states in SBML but can be factorized into one in OABS. Transitions that apply to all such states do not have to be repeated 12 times, which would be a waste of time.Here's an example of state with two substates of different types:	new Cell=		new State(			Function=				some TCellFunction			Age=				some TCellAge		)This means that an object can be composed of various subproperties which can influence their behavior. Substates can influence the behavior of an object by serving as guards for reactions and transitions, by providing coefficients used in transition rates, or by providing other substates to use as transition target.	DestroyUndefendedFortress=		new Reaction(			From=   [ some Fortress(DefenseState=NotProtected, AttackState=Attacked) ]			To=     [ ]			Rate=   0.5		)Because substates can also take numeric values, the number of states that can be possibly generated is potentially unbounded. An example of such unbounded state potential could be a Cell with an Age property that would be a number, and be incremented by one every time it is cloned. The age could then be used as an negatively-affecting factor in the cloning rate of the cell. The expressivity gained here is enormous. Firstly, you can get an infinite number of states, but you can also define your transitions in a generic way, making use of the exact value of agent-defined parameters.	new LocatedObject=		new State(			X = some number			Y = some number		)As explained previously, OABS support both Transitions and Reactions. Transitions differ from reactions in the sense they conserve the number of states, and can therefore be used for substates that do not live in sets, and can only evolve from one value to another. In our MilitaryUnit example, they would explain how a New unit could become Experienced, for instance. 	NewUnitsDifferentiation=		new Transition(			From= New			To=   Experienced			Rate= ...		)OABS supports pattern matching which allows to control whenever transitions or reactions apply very precisely. Destination states of transitions and reactions can be defined by copying arbitrarily information from the previous states, or incorporating computations or new values.	GrowthOfSafeCities=		new Reaction(			From=				[					some City(DefenseState=Protected, AttackState=NotAttacked)				]			To=				[					1 new[x]()					1 new[x](DefenseState=NotProtected)				]			Rate=				0.003		)To the contrary of SBML which will need to execute the transitions to all possible variants of a state, OABS will only work on variants that actually exist. This is actually necessary as the number of existing states can be unbounded (but the actual number of different states is bounded by the branching factor of the simulation and execution time).Finally, OABS use a text-based format that's easily readable and writable by humans, as opposed to XML and MathML which are complex to write and reserved to machines.## Advantages over SBMLThe languages supports things similar to templates, and allows for code reuse between models. Because it encourages encapsulation, it enables model combinations, which can improve the state of the art of simulation.Because it enables nested states, it is also more readable and decomposes the problems into smaller entities, enabling to fine tune each submodel independently by trying hypothesis in a relatively independent manner.By allowing unbounded numerical values as states, it also open the doors to other kinds of simulations where agents live in an open world. By allowing you to give units to numerical values -- like seconds : minutes : hours, and define their relationships, it makes your code safer (checking units) and easier to read (conversions between reference units and alias units done for you under the scenes).Finally, the file format is much more readable, and you don't need to train yourself to understand the models developed by someone else. After all, isn't that the goal of a model?