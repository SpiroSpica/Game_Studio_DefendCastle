# Game_Studio_DefendCastle
Game Scripts for Defend the Castle

2017-12-03

##Working

Monster
- Path Finding
- Collision & Damage to Castle / Destroyed after it
- Die if hp <=0

Castle
- HP System (fall apart at HP 50% && go down at HP 0%)
- Collision & Damage Receiving

System
- Summon Monster

Turret
- Seek for Monster
- Fire energyBall

2017-12-04

System
- Now Check Game Ends
  It counts the remaining Monster Objects and castle HP
  game ends if castle's hp <=0 or remaining Monster number == 0
  system stops spawning monsters and monsters stop moving

- Gold system
  gold is accumulated certain amount at regular time
  if monster dies, gold increases
  certain amount of gold is needed to summon turret

UI
- show current gold and remaining monster number

2017-12-10

System
- Now Checks save
- Save is done every time the player won the stage
- For Stage, there's red, blue, white (cannot access, cleared, where to go)

Monster / Turret / Stage
- Ready for multiple kinds
- Monster have been tested
- Turret and Stage has to be tested to make sure

