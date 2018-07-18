# Ideas / Not decided

- Attacks inside hull move in slow mode
- Scripted levels or procedural?
- Deform hull (test spring) (use a single sprite for hull and shader to match)
- Attack independent of movement (BoI, EtG)

# Decided

- X levels (+ new game plus, if we have time)
- 8 directions (instead of free move)

# Roadmap

## Game

- [ ] Hull screen bounds
- [x] Player Movement
  - [x] Basic
  - [x] Hull (using center of ship. Can be more sophisticated using the
      colliders)
- [x] Bullets
  - [x] Hull interaction
  - [x] Enemy interaction
  - [x] Player interaction
- [ ] Enemy
  - [ ] Destroy after get out of screen
  - [x] Dummy enemy
  - [x] ZigZag enemy
  - [x] Spiked
  - [ ] First boss
    - [ ] Movement (use yield to add complexity)
    - [ ] Attack (use yield to add complexity)
- [x] Player health (hull shrink)
  - [x] Enemy attack interaction (invul time)
- [ ] Level design
  - [ ] Tutorial
  - [ ] General level generation / Level scripting
  - [ ] Level progression
- [ ] Power ups
- [ ] UI
  - [ ] Basic UI
  - [ ] Game Over

## Art

- [ ] GFX
  - [ ] General graphics feel
  - [ ] Player
  - [ ] Hull
  - [ ] Enemy
    - [ ] Basic enemy
    - [ ] Basic enemy attack
  - [ ] Player attack
- [ ] SFX
  - [ ] Music
  - [ ] Player attack
  - [ ] Enemy attack
  - [ ] Player damage
  - [ ] Player explosion
  - [ ] Enemy 

## Extra

- [ ] Extra
  - [ ] More player attacks
  - [ ] More enemies
  - [ ] Boss low damage behaviour change
  - [ ] Boss levels (more attacks, more movements)
  - [ ] More power ups


# BUGS

- [ ] Unity
  - [ ] "Screen position out of view frustum" -> Probably because of camera
      size?
- [ ] Movement + attack: up left shooting doesnt work
