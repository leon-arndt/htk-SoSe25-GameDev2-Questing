VAR completable_coin10quest = false
VAR completed_coin10quest = false

Can anyone hear me?
Hi my name is Ralf nice to meet you, please give me some coins. 
This is my destiny in life - I need coins. # addItem Coin

 * Give me the quest
    -> Accept
 * No, leave me alone
    -> Decline
* I want to give you a coin
      -> GiveCoin
* {completable_coin10quest} I finished your quest, here you go!
   -> CompleteQuest
* {completed_coin10quest} Do you have any friends?
   -> AddQuestMeetMona


=== Accept ===
Ok, here's your quest # startQuest Coin10Quest
-> END


=== Decline ===
Ok, let me know if you change your mind.
-> END

=== CompleteQuest === 
# completeQuest Coin10Quest
You're the best, thanks for the coins!
-> END

=== AddQuestMeetMona === 
I do, please go and find Mona # startQuest TalkToMona
-> END


=== GiveCoin ===
Thanks for the coin #removeItem Coin
-> END