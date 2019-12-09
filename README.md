# Wasabi vs Samourai Various Stats

# I. CoinJoin Stats

`dotnet run -- --rpcuser=user --rpcpassword=password`

## Results

```
2019.6
Wasabi transaction count:                   528
Samourai transaction count:                 543
Wasabi total volume:                        17109 BTC
Samourai total volume:                      75 BTC
Wasabi total mixed volume:                  10569 BTC
Samourai total mixed volume:                75 BTC
Wasabi anonset weighted volume mix score:   346819
Samourai anonset weighted volume mix score: 376

2019.7
Wasabi transaction count:                   563
Samourai transaction count:                 1329
Wasabi total volume:                        13358 BTC
Samourai total volume:                      218 BTC
Wasabi total mixed volume:                  8402 BTC
Samourai total mixed volume:                218 BTC
Wasabi anonset weighted volume mix score:   308361
Samourai anonset weighted volume mix score: 1094

2019.8
Wasabi transaction count:                   563
Samourai transaction count:                 761
Wasabi total volume:                        35362 BTC
Samourai total volume:                      166 BTC
Wasabi total mixed volume:                  24366 BTC
Samourai total mixed volume:                166 BTC
Wasabi anonset weighted volume mix score:   558039
Samourai anonset weighted volume mix score: 832

2019.9
Wasabi transaction count:                   600
Samourai transaction count:                 898
Wasabi total volume:                        26200 BTC
Samourai total volume:                      200 BTC
Wasabi total mixed volume:                  17485 BTC
Samourai total mixed volume:                200 BTC
Wasabi anonset weighted volume mix score:   419303
Samourai anonset weighted volume mix score: 1000

2019.10
Wasabi transaction count:                   583
Samourai transaction count:                 1542
Wasabi total volume:                        16572 BTC
Samourai total volume:                      301 BTC
Wasabi total mixed volume:                  10779 BTC
Samourai total mixed volume:                301 BTC
Wasabi anonset weighted volume mix score:   418963
Samourai anonset weighted volume mix score: 1506
```

## Explanation

### How accurate are the numbers?

The numbers are accurate. The two wallet's coinjoins can be identified with close to 100% accuracy.

###  transaction count

The number of coinjoin transactions those were done.

###  total volume

The amount of BTC that went through the coinjoins transactions.

###  total mixed volume

The amount of BTC that went through the coinjoins transactions with equal outputs.


###  anonset weighted volume mix score

The amount of BTC that went through the coinjoins transactions with equal outputs, and each mixed output is multiplied by the number of other equal outputs.
For example a transaction that has outputs of `1,2,2,4,4,4` has a score of `2*2 + 4*3`.

## II. Development Activity Stats

Tool: https://github.com/kylemacey/repo-contrib-graph

![](https://i.imgur.com/IEi4677.png)
