INCLUDE playerStats
INCLUDE enemies



Hello there, welcome to Delegate Quest! 

An evil and maniacal Dragon has been spotted near the Volcano, and he threatens to destroy the Kingdom of Rezmissly in seven days. ONLY YOU CAN STOP HIM!

But you are not strong enough yet to defeat him. You must become stronger by training against weaker monsters, gaining levels and better equipment along the way!

You may each day choose three places to visit : one in the morning, one in the afternoon and one in the evening. After that, you will rest at the inn, and the next day will begin.

Do your best to beat the Dragon as quickly as possible. Start by training in the sewers, then go sell your loot at the blacksmith and buy some better gear. Good Luck!

-> day

=== day
#musicSwitch choice

{   
    - DAYNUMBER == 1:
        This is the first day.
    - DAYNUMBER == 2:
        This is the second day.
    - DAYNUMBER == 3:
        This is the third day.
    - DAYNUMBER == 4:
        This is the fourth day.
    - DAYNUMBER == 5:
        This is the fifth day.
    - DAYNUMBER == 6:
        This is the sixth day.
    - DAYNUMBER == 7:
        This is the last day.
}

What will you do today?
#choose #skipChoice #skipText

+ [Validate choices]

-> MORNING_PLACE ->
It is now the Afternoon.

-> AFTERNOON_PLACE ->
It is now the Evening.

-> EVENING_PLACE ->

{   
    - DAYNUMBER == 1:
        -> night1 ->
    - DAYNUMBER == 2:
        -> night2 ->
    - DAYNUMBER == 3:
        -> night3 ->
    - DAYNUMBER == 4:
        -> night4 ->
    - DAYNUMBER == 5:
        -> night5 ->
    - DAYNUMBER == 6:
        -> night6 ->
    - DAYNUMBER == 7:
        -> night7 ->
}

~ DAYNUMBER++

-> day


=== sewers
#musicSwitch sewers

You are in the sewers.

-> sewersgen ->

-> encounter ->

#musicSwitch sewers

You are still in the sewers.

-> sewersgen ->

-> encounter ->

#musicSwitch sewers

You are still in the sewers.

-> sewersgen ->

-> encounter ->

#musicSwitch sewers

You exit the sewers.

->->

=== sewersgen
~ RANDOMENEMY = RANDOM(1, 2)
{
    - RANDOMENEMY == 1:
        ~CURRENTENEMYNAME = SLIMENAME
        ~CURRENTENEMYHP = SLIMEHP
        ~CURRENTENEMYATTACK = SLIMEATTACK
        ~CURRENTENEMYDEFENCE = SLIMEDEFENCE
        ~CURRENTENEMYFAST = SLIMEFAST
        ~CURRENTENEMYEXPERIENCEVALUE = SLIMEEXPERIENCEVALUE
        ~CURRENTENEMYDROPNAME = SLIMEDROPNAME
        ~CURRENTENEMYDROPVALUE = SLIMEDROPVALUE
    - RANDOMENEMY == 2:
        ~CURRENTENEMYNAME = RATNAME
        ~CURRENTENEMYHP = RATHP
        ~CURRENTENEMYATTACK = RATATTACK
        ~CURRENTENEMYDEFENCE = RATDEFENCE
        ~CURRENTENEMYFAST = RATFAST
        ~CURRENTENEMYEXPERIENCEVALUE = RATEXPERIENCEVALUE
        ~CURRENTENEMYDROPNAME = RATDROPNAME
        ~CURRENTENEMYDROPVALUE = RATDROPVALUE
}
->->

=== forest
#musicSwitch forest

You are in the forest.

->->

=== desert
#musicSwitch desert

You are in the desert.

->->

=== volcano
#musicSwitch volcano

You are in the volcano.

->->

=== encounter
#musicSwitch battle
#monsterSwitch

A {CURRENTENEMYNAME} draws near!
{
    -CURRENTENEMYFAST:
        ->enemyattack
    -else:
        ->playerattack
}

->->

=== playerattack

You hit the {CURRENTENEMYNAME}! You dealt {ATTACK - CURRENTENEMYDEFENCE} damage!

~ CURRENTENEMYHP = CURRENTENEMYHP - (ATTACK - CURRENTENEMYDEFENCE)
{
    -CURRENTENEMYHP <= 0:
        ->victory
}

->enemyattack


=== enemyattack

The {CURRENTENEMYNAME} hits you! You took {CURRENTENEMYATTACK - DEFENCE} damage!

~ CURRENTHP = CURRENTHP - (CURRENTENEMYATTACK - DEFENCE)


-> playerattack

=== victory
#musicSwitch victory
#killMonster

You defeated the {CURRENTENEMYNAME}.

~ EXPERIENCE = EXPERIENCE + CURRENTENEMYEXPERIENCEVALUE

{
    - EXPERIENCE >= NEXTLEVEL:
        -> levelUp ->
}

{
    - BAG >= 5:
        You have no room for another item, so you leave the {CURRENTENEMYDROPNAME}.
    - else:
    You obtained {CURRENTENEMYDROPNAME}.

    ~ BAG++
    {
        - ITEM1 == 0:
            ~ ITEM1 = CURRENTENEMYDROPVALUE
        - ITEM2 == 0:
            ~ ITEM2 = CURRENTENEMYDROPVALUE
        - ITEM3 == 0:
            ~ ITEM3 = CURRENTENEMYDROPVALUE
        - ITEM4 == 0:
            ~ ITEM4 = CURRENTENEMYDROPVALUE
        - ITEM5 == 0:
            ~ ITEM5 = CURRENTENEMYDROPVALUE
    }
}
->->

=== levelUp
#musicSwitch levelup

You leveled up!

~ LEVEL++
~ MAXHP = MAXHP + 5
~ CURRENTHP = CURRENTHP + 5
~ ATTACK++
~ DEFENCE++
~ EXPERIENCE = EXPERIENCE - NEXTLEVEL
~ NEXTLEVEL = NEXTLEVEL * 2

->->

=== defeat

->->

=== blacksmith
#musicSwitch blacksmith

You are at the blacksmith.

->->

=== tavern
#musicSwitch tavern

You are at the tavern.

->->

=== church
#musicSwitch church

You are at the church.

->->

=== night1

This is the first night.

->->

=== night2

This is the second night.

->->

=== night3

This is the third night.

->->

=== night4

This is the fourth night.

->->

=== night5

This is the fifth night.

->->

=== night6

This is the sixth night.

->->

=== night7

This is the last night.

Game Over.

-> DONE

->->
