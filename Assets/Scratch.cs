/*
 Welcome to Crime-Boss Tycoon (Kingpin?)

You are a crime boss. Well... You want to be. Maybe you'll be one eventually.

But right now you only have this one little guy who you trust with small jobs


You also have a "Headquarters" to manage
Options:
    - Hire (-money, +criminals)
    - Upgrades (-money)

You have a few resources to manage

    - Cash (upgrades, criminals, equipment, bribes)
    - Notice (chances of criminals being caught, better criminals available for hire)

Occasinally you might notice an opportunity for a event
Options:
    - Bank Robbery (+++money, +++notice)
    - "Absorb" a rival (++criminals)
    
Player options
    - Nerd: Bonus to tech jobs, cheaper tech upgrades
            Minus to intimidation; power

    - Bruiser: Bonus to power jobs, 

    - Slick: Bonus to recruitment, distractions, 

    - Shadow: Bonus to sneak, resistance to notice

You have a few stats
    - Power: Ability to beat people up, push past obstacles, move heavy objects
    - Stealth: Ability get in and out quietly, lock picking, pick pocketing
    - Tech: Ability with computers, security systems, hacking, gadgets
    - Charm: Ability to get people to trust you, pay attention to you, provide distractions, talk your way out of things


Game Loop
    -> Load Game
        Starting stats:
            0 criminals
            0 suspicion
            20$
        
        
 
 */

// When a criminal is added to any job list
// start their "work"
// calculate success rate, and work speeds per criminal

// each success returns cash
// each failure returns suspicion

// TODO - NEED TO ADJUST ALL VALUES
// SOME JOBS SHOULD WORK ON LONGER TICKS

// Each job needs its own pros/cons.
// Pick Pocket: 
//      quick completion time +
//      low suspicion + 
//      low reward - 
//      average success rate ~
// Hacking
//      lower success rate -
//      higher reward +
//      low suspicion +
//      slower completion time? -
// Mugging: 
//      fast completion +
//      high suspicion -
//      high reward +
//      average success? ~
// Con Artist:
//      longest completion -
//      highest reward +
//      average suspicion ~
//      average success rate? ~