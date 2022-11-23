# ManageBotPlus

## ManageBot Plus is an extension to the [original ManageBot](http://invite.managebot.xyz). 


If you want to host this bot yourself, you need add two files, called `allowedGuilds.json` and `token.json` in the `Config` folder.
You also need to make sure, that they are Embedded Ressources, otherwise the Stream we're reading the files out with will be `null`.

The structure of `token.json` is as follows
```json
{
  "token": "token here",
  "testToken": "test token here"
}
```
and `allowedGuilds.json`
```json
{
  "allowedGuilds": [
    123, 
    123, 
    123 
  ]
}
```

you are also required to have knowledge about on how to run projects using .NET 7.
