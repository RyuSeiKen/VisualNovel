INCLUDE playerStats
INCLUDE enemies

#musicSwitch intro

Hello there, welcome to Delegate Quest! 

An evil and maniacal Dragon has been spotted near the Volcano, and he threatens to destroy the Kingdom of Rezmissly in seven days. ONLY YOU CAN STOP HIM!

But you are not strong enough yet to defeat him. You must become stronger by training against weaker monsters, gaining levels and better equipment along the way!

You may each day choose three places to visit : one in the morning, one in the afternoon and one in the evening. After that, you will rest at the inn, and the next day will begin.

Do your best to beat the Dragon as quickly as possible. Start by training in the sewers, then go sell your loot at the blacksmith and buy some better gear. Good Luck!

-> day

=== day
-> daystart ->

What will you do today?
#choose #skipChoice #skipText

+ [Validate choices]

-> MORNING_PLACE ->
The sun is now high in the sky, as the afternoon begins.

-> AFTERNOON_PLACE ->
The red sky indicates the beginning of the evening.

-> EVENING_PLACE ->
The sun has set. You feel tired and go rest at the inn.

-> dayend ->

~ DAYNUMBER++

-> day

=== daystart
#musicSwitch choice

{   
    - DAYNUMBER == 1:
        Your adventure starts today. You only have 7 days before the Dragon launches his ultra breath attack on the kingdom, obliterating everything, you must act quickly.
    - DAYNUMBER == 2:
        Have you tried going to the tavern? People who need some monsters out of the way will give good money to someone willing to kill their target. Give it a shot once in a while.
    - DAYNUMBER == 3:
        The blacksmith only sells armour, but some enemies always strike first, which means you'll certainly get hit no matter how strong you are. Having good defences is essential.
    - DAYNUMBER == 4:
        If you're feeling a little low on health, don't hesitate to visit the church. The priests will be more than happy to see you, and the holy water is really effective at healing wounds.
    - DAYNUMBER == 5:
        Leveling up increases your strength, which is necessary to kill monsters faster and to deal damage to enemies with better defences. Keep getting those levels!
    - DAYNUMBER == 6:
        This is your last day to train before heading for the volcano. You'll probably want to heal and go shopping tomorrow before starting the climb.
    - DAYNUMBER == 7:
        Today is your last chance to slay the Dragon. He has now nearly finished preparing for his ultra breath attack, and he certainly won't wait before using it.
}
->->

=== sewers
#musicSwitch sewers

You enter the sewers beneath the town. The stench is ubelievable, but you know this is the best place to gain combat experience without risking fighting something much too strong.

-> sewersgen ->

-> encounter ->

#musicSwitch sewers

Your exploration of the sewers continues. You keep hearing the sounds the slimes and the rats make. Perhaps buying armour at some point would mitigate some of their damage.

-> sewersgen ->

-> encounter ->

#musicSwitch sewers

{
    - SEWERSQUESTACCEPTED == true && SEWERSBOSSDEFEATED == false:
     You remember the request of the young man at the tavern as you smell something radically different from what you've been used to. You prepare yourself, as this will be your first major challenge.
        ~CURRENTENEMYNAME = LIZARDNAME
        ~CURRENTENEMYHP = LIZARDHP
        ~CURRENTENEMYATTACK = LIZARDATTACK
        ~CURRENTENEMYDEFENCE = LIZARDDEFENCE
        ~CURRENTENEMYFAST = LIZARDFAST
        ~CURRENTENEMYEXPERIENCEVALUE = LIZARDEXPERIENCEVALUE
        ~CURRENTENEMYDROPNAME = LIZARDDROPNAME
        ~CURRENTENEMYDROPVALUE = LIZARDDROPVALUE
        -> encounter ->
   

    - else :
     The smell is now getting truly unbearable. After the next encounter, you'll be taking your leave, because at this point you've seen enough pests for a lifetime.
        -> sewersgen ->

        -> encounter ->
}


#musicSwitch sewers

You exit the sewers pretty exhausted. It was good practice, but you're pretty sure this isn't going to make you as strong as you wish. Perhaps you should go to the tavern to find some work.

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

You enter the forest located West of the town. The trees make omnious sounds as the branches crack and the wind blows through the leaves... you are not alone in those woods.

-> forestgen ->

-> encounter ->

#musicSwitch forest

As you continue looking around the forest, you are reminded of the time you spent playing with your brother and sister when you were little. If only you could go back to those times...

-> forestgen ->

-> encounter ->

#musicSwitch forest

{
    - FORESTQUESTACCEPTED == true && FORESTBOSSDEFEATED == false:
    You spot some weird fluid on the ground. The old lady in the tavern did mention a giant poisonous plant... You hear something approaching, get ready to fight.
        ~CURRENTENEMYNAME = PLANTNAME
        ~CURRENTENEMYHP = PLANTHP
        ~CURRENTENEMYATTACK = PLANTATTACK
        ~CURRENTENEMYDEFENCE = PLANTDEFENCE
        ~CURRENTENEMYFAST = PLANTFAST
        ~CURRENTENEMYEXPERIENCEVALUE = PLANTEXPERIENCEVALUE
        ~CURRENTENEMYDROPNAME = PLANTDROPNAME
        ~CURRENTENEMYDROPVALUE = PLANTDROPVALUE
        -> encounter ->
   

    - else :
     You've had enough of this forest. The plant life is surprisingly violent, and you've heard rumours of a certain man eating plant that mercilessly kills those who stay here too long.
        -> forestgen ->

        -> encounter ->
}

#musicSwitch forest

You finally leave this creepy place. The fact that you survived for so long inside it proves how much you've improved recently. But you know that the hard part of your jouney is yet to come.

->->

=== forestgen
~ RANDOMENEMY = RANDOM(1, 2)
{
    - RANDOMENEMY == 1:
        ~CURRENTENEMYNAME = MUSHROOMNAME
        ~CURRENTENEMYHP = MUSHROOMHP
        ~CURRENTENEMYATTACK = MUSHROOMATTACK
        ~CURRENTENEMYDEFENCE = MUSHROOMDEFENCE
        ~CURRENTENEMYFAST = MUSHROOMFAST
        ~CURRENTENEMYEXPERIENCEVALUE = MUSHROOMEXPERIENCEVALUE
        ~CURRENTENEMYDROPNAME = MUSHROOMDROPNAME
        ~CURRENTENEMYDROPVALUE = MUSHROOMDROPVALUE
    - RANDOMENEMY == 2:
        ~CURRENTENEMYNAME = TREENAME
        ~CURRENTENEMYHP = TREEHP
        ~CURRENTENEMYATTACK = TREEATTACK
        ~CURRENTENEMYDEFENCE = TREEDEFENCE
        ~CURRENTENEMYFAST = TREEFAST
        ~CURRENTENEMYEXPERIENCEVALUE = TREEEXPERIENCEVALUE
        ~CURRENTENEMYDROPNAME = TREEDROPNAME
        ~CURRENTENEMYDROPVALUE = TREEDROPVALUE
}
->->

=== desert
#musicSwitch desert

You walk into the desert found East of the town. Nobody goes there anymore, but it used to be a place where merchants had to go in order to reach other cities in a short time as possible.

-> desertgen ->

-> encounter ->

#musicSwitch desert

After being in here for a while, it becomes quite clear why nobody comes here anymore. Between the burning heat and the very dangerous monster, it's no wonder only a true wrrior would venture here.

-> desertgen ->

-> encounter ->

#musicSwitch desert

{
    - DESERTQUESTACCEPTED == true && DESERTBOSSDEFEATED == false:
    You notice something shiny on a dune, not too far away. You know all too well what it is, given the information you received from the soldier at the tavern. You brace yourself...
        ~CURRENTENEMYNAME = SOLDIERNAME
        ~CURRENTENEMYHP = SOLDIERHP
        ~CURRENTENEMYATTACK = SOLDIERATTACK
        ~CURRENTENEMYDEFENCE = SOLDIERDEFENCE
        ~CURRENTENEMYFAST = SOLDIERFAST
        ~CURRENTENEMYEXPERIENCEVALUE = SOLDIEREXPERIENCEVALUE
        ~CURRENTENEMYDROPNAME = SOLDIERDROPNAME
        ~CURRENTENEMYDROPVALUE = SOLDIERDROPVALUE
        -> encounter ->
   

    - else :
    You're starting to run out of water, it's about time you headed back. The monsters here are strong, but it's not like they really have a choice, considering the harsh conditions.
        -> desertgen ->

        -> encounter ->
}

#musicSwitch desert

You leave the desert, wondering how many people lost their lives trying to cross it. Now that you've survived this hellish place, there's only more step before fighting the mighty Dragon.

->->

=== desertgen
~ RANDOMENEMY = RANDOM(1, 2)
{
    - RANDOMENEMY == 1:
        ~CURRENTENEMYNAME = SCORPIONNAME
        ~CURRENTENEMYHP = SCORPIONHP
        ~CURRENTENEMYATTACK = SCORPIONATTACK
        ~CURRENTENEMYDEFENCE = SCORPIONDEFENCE
        ~CURRENTENEMYFAST = SCORPIONFAST
        ~CURRENTENEMYEXPERIENCEVALUE = SCORPIONEXPERIENCEVALUE
        ~CURRENTENEMYDROPNAME = SCORPIONDROPNAME
        ~CURRENTENEMYDROPVALUE = SCORPIONDROPVALUE
    - RANDOMENEMY == 2:
        ~CURRENTENEMYNAME = LIONNAME
        ~CURRENTENEMYHP = LIONHP
        ~CURRENTENEMYATTACK = LIONATTACK
        ~CURRENTENEMYDEFENCE = LIONDEFENCE
        ~CURRENTENEMYFAST = LIONFAST
        ~CURRENTENEMYEXPERIENCEVALUE = LIONEXPERIENCEVALUE
        ~CURRENTENEMYDROPNAME = LIONDROPNAME
        ~CURRENTENEMYDROPVALUE = LIONDROPVALUE
}
->->

=== volcano
#musicSwitch volcano

You start climbing the massive volcano north of town. At the top, the Dragon is focusing his magic power in order to destroy the Kingdom and it's inhabitants... But he won't be able to if he's dead.

-> volcanogen ->

-> encounter ->

Those fire spirits are quite powerful and would certainly have destroyed you instantly a week ago. But you're not the same  as you were before. Now you're a hero.

-> volcanogen ->

-> encounter ->

As you're about to reach the summit, you think of your family living  in the South, what you'll tell them and what you'll do with them after you've saved the kingdom... But now is the time to focus.

~CURRENTENEMYNAME = DRAGONNAME
~CURRENTENEMYHP = DRAGONHP
~CURRENTENEMYATTACK = DRAGONATTACK
~CURRENTENEMYDEFENCE = DRAGONDEFENCE
~CURRENTENEMYFAST = DRAGONFAST
~CURRENTENEMYEXPERIENCEVALUE = DRAGONEXPERIENCEVALUE
~CURRENTENEMYDROPNAME = DRAGONDROPNAME
~CURRENTENEMYDROPVALUE = DRAGONDROPVALUE

-> encounter ->

->->

=== volcanogen
~CURRENTENEMYNAME = FLAMENAME
~CURRENTENEMYHP = FLAMEHP
~CURRENTENEMYATTACK = FLAMEATTACK
~CURRENTENEMYDEFENCE = FLAMEDEFENCE
~CURRENTENEMYFAST = FLAMEFAST
~CURRENTENEMYEXPERIENCEVALUE = FLAMEEXPERIENCEVALUE
~CURRENTENEMYDROPNAME = FLAMEDROPNAME
~CURRENTENEMYDROPVALUE = FLAMEDROPVALUE
->->

=== encounter
#monsterSwitch
{
    - CURRENTENEMYNAME == "Maniacal and Evil Dragon":
        #musicSwitch dragon
        This is it! The {CURRENTENEMYNAME} roars at you loudly and prepares his fire breath. This is final battle, you can do this!
    - CURRENTENEMYNAME == "Loony Lizard" || CURRENTENEMYNAME == "Poisonous Plant" || CURRENTENEMYNAME == "Solid Soldier":
        #musicSwitch boss
        The {CURRENTENEMYNAME} stands before you! You've found him now, but he won't go down without a fight.
    - else:
        #musicSwitch battle
        A {CURRENTENEMYNAME} draws near!
}

{
    -CURRENTENEMYFAST:
        -> enemyattack
    -else:
        -> playerattack
}

->->

=== playerattack
#playEffect playerhit
~ DAMAGE = ATTACK - CURRENTENEMYDEFENCE
{
    - DAMAGE < 0:
        ~ DAMAGE = 0
}

You hit the {CURRENTENEMYNAME}! You dealt {DAMAGE} damage!

~ CURRENTENEMYHP = CURRENTENEMYHP - DAMAGE
{
    -CURRENTENEMYHP <= 0:
        -> victory
}

-> enemyattack


=== enemyattack
#playEffect enemyhit
~ DAMAGE = CURRENTENEMYATTACK - DEFENCE
{
    - DAMAGE < 0:
        ~ DAMAGE = 0
}

The {CURRENTENEMYNAME} hits you! You took {DAMAGE} damage!

~ CURRENTHP = CURRENTHP - DAMAGE

{
    -CURRENTHP <= 0:
        ~ CURRENTHP = 0
        -> defeat
}

-> playerattack

=== victory
#killMonster
{
    - CURRENTENEMYNAME == "Maniacal and Evil Dragon":
        #musicSwitch theend
        Finally, the {CURRENTENEMYNAME} falls before your mighty blade. It took you a lot of courage and determination to get through all this, but you have now prevailed. Congratulations!
        
        -> END
    - CURRENTENEMYNAME == "Loony Lizard" || CURRENTENEMYNAME == "Poisonous Plant" || CURRENTENEMYNAME == "Solid Soldier":
        #musicSwitch triumph
        The {CURRENTENEMYNAME} is no longer of this world! You should return to the tavern in order to report your accomplishments.
        {
        - SEWERSBOSSDEFEATED == false:
            ~ SEWERSBOSSDEFEATED = true
        - FORESTBOSSDEFEATED == false:
            ~ FORESTBOSSDEFEATED = true
        - DESERTBOSSDEFEATED == false:
            ~ DESERTBOSSDEFEATED = true
        }
    - else:
        #musicSwitch victory
            You defeated the {CURRENTENEMYNAME}.
}


~ EXPERIENCE = EXPERIENCE + CURRENTENEMYEXPERIENCEVALUE

{
    - EXPERIENCE >= NEXTLEVEL:
        -> levelUp ->
}

{
    - BAG >= 5:
        You have no room for another item, so you leave the {CURRENTENEMYDROPNAME} on the ground, hoping someone else will find some use for it.
    - else:
        You obtained {CURRENTENEMYDROPNAME}. You put it in your bag, expecting to sell it for some gold at the blacksmith's.

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

You leveled up! You've become stronger and more resilient. Keep it up!

~ LEVEL++
~ MAXHP = MAXHP + 5
~ CURRENTHP = CURRENTHP + 5
~ ATTACK++
~ EXPERIENCE = EXPERIENCE - NEXTLEVEL
~ NEXTLEVEL = NEXTLEVEL * 2

->->

=== defeat
#musicSwitch death
#killMonster
You died, and therefore, no one will be able to prevent the destruction of the Kingdom of Rezmissly...

Note that if you would like to try again, feel free to hold the CTRL button to fast forward through text.

-> END

=== blacksmith
#musicSwitch blacksmith
#placeSwitch blacksmith

You enter the blacksmith's shop, and he gives you a warm welcome. This blacksmith only sells armour, but his work seems to be of excellent quality.

{
    - BAG > 0:
        You sell all your loot and receive {ITEM1 + ITEM2 + ITEM3 + ITEM4 + ITEM5} gold. The blacksmith thanks you, as he needs those materials in order to work efficiently.
        ~ MONEY = MONEY + ITEM1 + ITEM2 + ITEM3 + ITEM4 + ITEM5
        ~ ITEM1 = 0
        ~ ITEM2 = 0
        ~ ITEM3 = 0
        ~ ITEM4 = 0
        ~ ITEM5 = 0
        ~ BAG = 0
}

{
    - MONEY > 10 && ARMOURLEVEL < 1:
        You buy some basic armour for 10 gold. It won't be much against true threats, but it's certain to be of use against the weak monsters in the sewers.
        ~ DEFENCE = DEFENCE + 1
        ~ ARMOURLEVEL++
        ~ MONEY = MONEY - 10
    - MONEY > 20 && ARMOURLEVEL < 2:
        You buy some advanced armour for 20 gold. This set of armour is the standard equipment of the Rezmissly soldiers, it seems simple but quite effective.
        ~ DEFENCE = DEFENCE + 2
        ~ ARMOURLEVEL++
        ~ MONEY = MONEY - 20
    - MONEY > 40 && ARMOURLEVEL < 3:
        You buy some superb armour for 40 gold. The blacksmith tells you this used to belong to a hero king who is rumoured to have survived dragon fire with it. 
        ~ DEFENCE = DEFENCE + 3
        ~ ARMOURLEVEL++
        ~ MONEY = MONEY - 40
    - else:
        You don't have enough money to buy anything, unfortunately.
}

It was nice chatting with the blacksmith about armour, but you must be on your way now. Perhaps you'll come back here once you have more gold to spend.
#killMonster

->->

=== tavern
#musicSwitch tavern
#placeSwitch tavern

You are at the tavern. This place never changes. You recognise quite a few people, but it the feeling in the air never gets old. You look for someone in need of your services.

{
    -  LEVEL == 1:
    Unfortunately, when asked if you've ever dealt with monsters, people start noticing your inexperience. Perhaps you should come back after getting a little experience from fighting the monsters in the sewers.
    
    -  SEWERSQUESTACCEPTED == false:
    A young man appears to want something of you :
    
    Hello, sorry for bothering you, do you have a moment?
    
    The other day, my little brother was attacked by some crazy lizard while he was having fun in the river. He escaped fine, but I wouldn't want something like this happening again.
    
    I'm pretty sure that critter's hiding in the sewers most of the time. I'd be very grateful if you could kill it for me. Come back to me once you're done, and I'll give you something good.
    
    #musicSwitch quest

    Quest Accepted. You must kill the Loony Lizard.
        ~ SEWERSQUESTACCEPTED = true
    #musicSwitch tavern

    -  SEWERSQUESTACCEPTED == true && SEWERSBOSSDEFEATED == false:
    The young man from last time notices you and asks :
    
    Hey, you don't seem to be done yet, are you?
    
    Perhaps I should have asked the guards to deal with this matter. No, that would've cost me much more money. Please, could you do me a favor and kill this monster?
    
    I really feel worried knowing it could strike at any time those poor children who just want a little fun by the water.
    
    -  SEWERSQUESTACCEPTED == true && SEWERSBOSSDEFEATED == true && SEWERSQUESTCOMPLETE == false:
    You tell your story to the young man, who now looks overjoyed :
    
    Great job, I knew I could count on you. Now that's one less thing for me to worry about. Here's your payment.
    
    I used to train in order to join the army once. My father was a general, and I wanted to be just like him. But now that I've found a less risky job, I've no use for his sword.
    
    Between you and me, I'm aware of the whole dragon issue. Perhaps you're the one who'll be able to deal with it. In any case, be safe, and I hope this sword brings luck.
    
    #musicSwitch accomplished

    Quest Complete. You've obtained a great sword!
        ~ ATTACK = ATTACK + 2
        ~ SEWERSQUESTCOMPLETE = true
    #musicSwitch tavern
    
    -  FORESTQUESTACCEPTED == false:
    An old lady approaches you from behind :
    
    Please, will you hear my request, my dear?
    
    Many years ago, me and my husband lived in the forest to the West. One day, we were attacked by an extremely aggressive plant. My husband protected me that day, and only I made it out alive.
    
     Would you please avenge my husband? I'm willing to pay some good money to anyone who kills this plant. Be sure to be prepared, it has a very powerful poison that can cause intense pain.  
    
    #musicSwitch quest

    Quest Accepted. You must kill the Poisonous Plant.
        ~ FORESTQUESTACCEPTED = true
    #musicSwitch tavern

    -  FORESTQUESTACCEPTED == true && FORESTBOSSDEFEATED == false:
    The old lady you know comes from behind, once again :
    
    You haven't found the plant yet?
    
    Well, it's not like it matters much anyway. Soon I'll pass away with and I'll get to see my husband again. If that Dragon doesn't kill me soon, time will.
    
    It's a shame really. To think such an evil creature is allowed to roam freely while this town will be destroyed soon...
    
    -  FORESTQUESTACCEPTED == true && FORESTBOSSDEFEATED == true && FORESTQUESTCOMPLETE == false:
    You find the old lady and tell her how the hunt went :
    
    Praise the almighty, I felt the evil run away the moment that monster was no more! Thank you so much.
    
    The forest wasn't always such a dangerous place. Everything changed for the worse after the Kingdom attempted some magical experiments that didn't go too well.
    
    Anyway, this doesn't concern you. Here's a magic potion I brewed for you. I used to make it for my husband all the time. It will make you much more resilient to damage. Have a safe trip.
    
    #musicSwitch accomplished

    Quest Complete. You've grown more resilient!
        ~ MAXHP = MAXHP + 10
        ~ CURRENTHP = CURRENTHP + 10
        ~ FORESTQUESTCOMPLETE = true
    #musicSwitch tavern
    
    -  DESERTQUESTACCEPTED == false:
    An experienced soldier hails you and asks :
    
    Do you have what it takes to beat the Dragon?
    
    You seem competent. If you complete my request, I'll give you something extra in order to make your mission easier. But it won't be an easy mission, so be careful.
    
    One of my old friends died crossing the Eastern desert. We found his body, but it appears some kind of magic possessed his armour afterwards. I'd likve you to put it to rest. 
    
    #musicSwitch quest

    Quest Accepted. You must kill the Solid Soldier.
        ~ DESERTQUESTACCEPTED = true
    #musicSwitch tavern

    -  DESERTQUESTACCEPTED == true && DESERTBOSSDEFEATED == false:
    The soldier doesn't seem pleased  :
    
    Do you think you have time to screw around?
    
    People are starting to have faith again, now that a decent fighter is helping everyone around those parts. Don't throw away your potential by acting rash or being lazy.
    
    I know you can do it. You just have to believe in yourself. Now go in the desert and get the job done.
    
    -  DESERTQUESTACCEPTED == true && DESERTBOSSDEFEATED == true && DESERTQUESTCOMPLETE == false:
    The soldier already knows what happened :
    
    I can see in your eyes that you've succeeded in my challenge. Congratulations.
    
    This shield belongs to my old friend, the one whose armour you just destroyed. It's enchanted by a mysterious stone that makes it especially resistant to fire.
    
    Oh, it looks like you've showed how willing you are to help others. Maybe the church will be able to bless your weapon now. In any case, please beat that Dragon form me.
    
    #musicSwitch accomplished

    Quest Complete. You've obtained an enchanted shield!
        ~ DEFENCE = DEFENCE + 4
        ~ DESERTQUESTCOMPLETE = true
    #musicSwitch tavern
}


After having a nice drink at the counter and talking about various things with the bartender, you remembered your quest wasn't going to finish itself. Time to go. 
#killMonster
->->

=== church
#musicSwitch church
#placeSwitch church

You enter the church of light. The priests are currently seeking protection from the Dragon by praying. Though you have faith in god, you know now is the time to act yourself.

You drink some of the holy water that's made available to the general public. You feel a surge an energy coming from within, and you feel ready to take on harder challenges.

#musicSwitch cure

Your health was completely restored! That felt great!

~ CURRENTHP = MAXHP

#musicSwitch church
{
    - DESERTQUESTCOMPLETE == true:
    The priests feel benevolence coming from you. They all start praying around you. You feel the power of God inside your weapon. The power compels you to go to the volcano. You might be able to hurt the Dragon now.
    
    #musicSwitch bless

    Your weapon is blessed with divine energy!

~ ATTACK = ATTACK * 2
    #musicSwitch church

}
You pray a little before leaving the church. The priests bid you farewell, hoping your efforts will be of more use than their praying. You have a Kingdom to save, it's time to get things done.
#killMonster
->->

=== dayend
#musicSwitch night
#placeSwitch inn

{   
    - DAYNUMBER == 1:
        Today was only the beginning. To think your're supposed to defeat a monster who has already eaten more than a third of the Kingdom's army... best not to think to much about it too much.
    - DAYNUMBER == 2:
        You realize how quickly time flies. Tomorrow you should start exploring the forst, in order to face actual threats, or else you'll never be ready to face the dangers of the volcano in time. 
    - DAYNUMBER == 3:
        You've received a letter from your family. Your parents recommend you run away before it's too late, but you've decided to stay committed to saving the kingdom.
    - DAYNUMBER == 4:
        You consider how much time you've spent in this small town, and how sad it would be if it was completely obliterated. You're more determined than ever before now.
    - DAYNUMBER == 5:
        You wonder how killing the Dragon could change your life. Will people praise you? Or will they be afraid? There's only one way to know...
    - DAYNUMBER == 6:
        Tomorrow is the last day before the Dragon attakcs. Even if you know you'll end up dead, you should still try to climb the volcano and die a hero.
    - DAYNUMBER == 7:
        You try to sleep during what will certainly be your last night. You can see the red light coming from the Volcano : the Dragon is ready to reduce everything to ashes.
        
 Game Over.
        -> END
}

#musicSwitch inn

You recovered thanks to a good night's rest. You're now ready for a new day full of adventure.

~ CURRENTHP = CURRENTHP + (MAXHP / 5)
{
    - CURRENTHP > MAXHP:
        ~ CURRENTHP = MAXHP
}
#killMonster
->->
