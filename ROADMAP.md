# Ideas / Not decided

- Attacks inside hull move in slow mode
- Boss shoot lasers to the sides
- Usable: Blank shot
- Usable: Dash (player moves forward, hull teleports/moves with player and kills
    enemies inside)
- Power up: extra shots
- Power up: wide shots
- Power up: piercing shots
- Joystick controls
- Free aim / Free movement

# Decided

## To do
- X levels (+ new game plus, if we have time)
- Ranking system: show enemies killed, damage taken and the ranking (FEDCBASP)
- Load next level
- Invincible player after level end
- Boss health bar
- Create polling system

## Ignored
- XXX Deform hull (test spring) (use a single sprite for hull and shader to match)

## Done
- 8 directions (instead of free move)
- Scripted levels

# Roadmap

## Game

- [x] Hull screen bounds
- [x] Player Movement
  - [x] Basic
  - [x] Hull (using center of ship. Can be more sophisticated using the
      colliders)
- [x] Bullets
  - [x] Hull interaction
  - [x] Enemy interaction
  - [x] Player interaction
- [ ] Enemy
  - [x] Destroy after get out of screen
  - [x] Dummy enemy
  - [x] ZigZag enemy
  - [x] Spiked
  - [x] First boss
  - [x] Boss entrance (after all units are dead)
- [x] Player health (hull shrink)
  - [x] Enemy attack interaction (invul time)
- [ ] Level design
  - [ ] Tutorial
  - [x] General level generation / Level scripting
  - [ ] Level progression
- [ ] Power ups
- [ ] UI
  - [ ] Basic UI
  - [x] Game Over
- [ ] Ranking system

## Art

- [ ] GFX
  - [ ] General graphics feel
  - [ ] Player
    - [ ] Turret
    - [x] Ship
  - [ ] Hull
  - [ ] Enemies
    - [ ] Dummy
    - [ ] Spiked
    - [ ] Mailgun
  - [x] Boss
    - [x] Quad
  - [ ] Player attack
  - [ ] Explosions
    - [ ] Attack explosion
  - [ ] Bullets
    - [ ] QuadBoss bullets
    - [ ] Player bullets
    - [ ] DummyEnemy bullets
    - [ ] Spiked bullets
    - [ ] Mailgun bullets
- [ ] Music
- [ ] SFX
  - [ ] Player attack
  - [ ] Enemy attack
  - [ ] Player damage
  - [ ] Player explosion
  - [ ] Enemy explosion

## Extra

- [ ] Extra
  - [ ] More player attacks
  - [ ] More enemies
  - [ ] Boss low damage behaviour change
  - [ ] Boss levels (more attacks, more movements)
  - [ ] More power ups
  - [ ] Parallax background


# BUGS

- [ ] Game Over reset, resets Keymap
- [ ] Ranking sometimes show before level ends
