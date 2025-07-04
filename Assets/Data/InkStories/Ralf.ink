VAR completable_coin10quest = false

Hi my name is Ralf nice to meet you, please give me some coins. This is my destiny in life - I need coins.

 * Give me the quest
    -> Accept
 * No, leave me alone
    -> Decline
* {completable_coin10quest} I finished your quest, here you go!
   -> CompleteQuest


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
