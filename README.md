# Unity_RPG

A 2D turn based RPG I'm making on my free time.

The goal of the game is to unlock new mutants, and make them stronger.

## Mutants

A mutant is a monster which can be created, and upgraded by making it fight in the arena.
Each mutant has a level, a list of skills (4 actives and 4 passives, with a maximum of 4 equipped at a time), a speed, a type (human, robot)...
A mutant also has a mutation level. The mutation level is a value between 0% and 100%, and is increased by 20% when making it mutate. This value is used to unlock new skills and mutants.

Mutants (and skills) can have a list of effects.
An effect is a modifier that can be applied before or after attacking or being attacked by a mutant.
They can apply a burning effect to targets (which deals damages after attacking), heal after attacking, create a shield...
Some depends on the damages dealt to enemies, some on the amount of damages received.

## Hub

The main hub allows to see the owned mutants, and choose their skills.
The first of the two buildings of the hub is used to Mutate and Evolve the mutant.

## Mutation

A mutant owns 3 different paths. A path is used to unlock new skills, and an Evolution. Unlocking something on a path costs dna, which is a currency earned by fighting.
The first path is unlocked by default.
A mutation unlocks other paths, and changes the mutant visually.

## Evolution

Once the option is unlocked on a path, a mutant can mutate into one of its descendant, with different visuals, skills, and statistics.

## Cloning

The second building of the hub is used to create new mutants. To create a new mutant, different components (embryo, mutants samples, temperature...) need to be used.
Depending on the components, unique mutants can be obtained.

## Battle

Battles are divided in Arenas. Finishing a battle unlocks the next in the same arena. Once an arena is completed, rewards are earned, and the next arena is unlocked.
Battles requires 3 different mutants to fight. 
In battle, mutants fight in an order determined by the speed of each mutant. For example, a mutant with a speed of 7 will attack 2 times before a mutant with a speed of 3 can attack.

Every mutant in the player's team has access to the active selected skills, each being represented by a button. Passive skills are automatically applied.

Once the battle is over, the player earns experience, credits, and dna.
Each mutant earn experience, and dna. When the dna bar of a mutant is full, it can mutate, evolve, or have a sample extracted, to be used in cloning.
