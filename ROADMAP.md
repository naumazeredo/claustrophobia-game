# Ideas / Not decided

- Attacks inside hull move in slow mode
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
- Invincible player after level end
- Boss health bar

## Ignored
- XXX Deform hull (test spring) (use a single sprite for hull and shader to match)

## Done
- 8 directions (instead of free move)
- Scripted levels
- Ranking system: show enemies killed, damage taken and the ranking (FEDCBASP)
- Load next level
- X levels (+ new game plus, if we have time)

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
- [x] Enemy
  - [x] Destroy after get out of screen
  - [x] Dummy enemy
  - [x] ZigZag enemy
  - [x] Spiked
  - [x] First boss
  - [x] Boss entrance (after all units are dead)
- [x] Player health (hull shrink)
  - [x] Enemy attack interaction (invul time)
- [x] Level design
  - [x] General level generation / Level scripting
  - [x] Level progression
- [ ] Power ups
- [ ] UI
  - [ ] Basic UI
  - [x] Game Over
- [x] Ranking system
- [ ] Credits

## Art

- [ ] GFX
  - [ ] General graphics feel
  - [ ] Player
    - [ ] Turret
    - [x] Ship
  - [ ] Hull
  - [x] Enemies
    - [x] Dummy
    - [x] Spiked
    - [x] Mailgun
  - [x] Boss
    - [x] Quad
  - [x] Player attack
  - [x] Explosions
    - [x] Attack explosion
  - [ ] Bullets
    - [x] QuadBoss bullets
    - [x] Player bullets
    - [x] DummyEnemy bullets
    - [ ] Spiked bullets
    - [x] Mailgun bullets
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
- [ ] EDITOR: Maximize On Play + Test Level on Play: pause and unpause (only
    works when Level Designer window is visible :/)
- [ ] EDITOR: Test Level On Play + 'R'eset after death does not work together
