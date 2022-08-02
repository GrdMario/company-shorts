## Run external API

- Our example communicates with https://github.com/GrdMario/company-cars. Read https://github.com/GrdMario/company-cars/blob/main/README.md if you want to start it.

## Run example

- git clone https://github.com/GrdMario/company-shorts.git
- Right click on Company.Shorts project
- Select Manage User Secrets - this will open secrets.json file
- Add following to secrets.json

```
{
  "CarServiceAdapterSettings:Url": "https://localhost:5000"
}
```

- Clean solution
- Build solution
- Start service

If for some reason you are not getting swagger endpoint, try to rebuilding it again.