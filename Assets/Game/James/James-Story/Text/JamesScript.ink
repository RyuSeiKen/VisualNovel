INCLUDE narativeFunctions
INCLUDE dailyChoices

Hello there, welcome to Delegate Quest! 

An evil and maniacal Dragon has been spotted near the Volcano, and he threatens to destroy the Kingdom of Rezmissly in seven days. ONLY YOU CAN STOP HIM!

But you are not strong enough yet to defeat him. You must become stronger by training against weaker monsters, gaining levels and better equipment along the way!

You may each day choose three places to visit : one in the morning, one in the afternoon and one in the evening. After that, you will rest at the inn, and the next day will begin.

Do your best to beat the Dragon as quickly as possible. Start by training in the sewers, then go sell your loot at the blacksmith and buy some better gear. Good Luck!

-> day

=== day
{   
    - DAY_NUMBER == 1:
        This is the first day.
    - DAY_NUMBER == 2:
        This is the second day.
    - DAY_NUMBER == 3:
        This is the third day.
    - DAY_NUMBER == 4:
        This is the fourth day.
    - DAY_NUMBER == 5:
        This is the fifth day.
    - DAY_NUMBER == 6:
        This is the sixth day.
    - DAY_NUMBER == 7:
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
    - DAY_NUMBER == 1:
        -> night1 ->
    - DAY_NUMBER == 2:
        -> night2 ->
    - DAY_NUMBER == 3:
        -> night3 ->
    - DAY_NUMBER == 4:
        -> night4 ->
    - DAY_NUMBER == 5:
        -> night5 ->
    - DAY_NUMBER == 6:
        -> night6 ->
    - DAY_NUMBER == 7:
        -> night7 ->
}

~ DAY_NUMBER++

-> day


=== sewers

You are in the sewers.

->->

=== forest

You are in the forest.

->->

=== desert

You are in the desert.

->->

=== volcano

You are in the volcano.

->->

=== blacksmith

You are at the blacksmith.

->->

=== tavern

You are at the tavern.

Do you want a drink?

->->

=== church

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
