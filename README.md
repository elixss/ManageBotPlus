# ManageBotPlus

## ManageBot Plus is an extension to the [original ManageBot](http://invite.managebot.xyz). 

You can invite the bot [here](https://canary.discord.com/api/oauth2/authorize?client_id=1043131771258159104&permissions=274878220288&scope=bot%20applications.commands)

If you want to host this bot yourself, you need to create a file called `token.json` and put it into the `Config` folder.
You also need to make sure, that they are Embedded Ressources, otherwise the Stream we're reading the files out with will be `null`.

The structure of `token.json` is as follows:
```json
{
  "token": "token here",
  "testToken": "test token here"
}
```

you are also required to have knowledge about on how to run projects using .NET 7.
