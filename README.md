# Wasabi vs Samourai coinjoin statistics

## Results

```
2019.6
Wasabi transaction count:                   528
Samourai transaction count:                 543
Wasabi total volume:                        17109 BTC
Samourai total volume:                      75 BTC
Wasabi total mixed volume:                  1558 BTC
Samourai total mixed volume:                15 BTC
Wasabi anonset weighted volume mix score:   10569
Samourai anonset weighted volume mix score: 75

2019.7
Wasabi transaction count:                   563
Samourai transaction count:                 1330
Wasabi total volume:                        13358 BTC
Samourai total volume:                      259 BTC
Wasabi total mixed volume:                  1097 BTC
Samourai total mixed volume:                51 BTC
Wasabi anonset weighted volume mix score:   8402
Samourai anonset weighted volume mix score: 259

2019.8
Wasabi transaction count:                   444
Samourai transaction count:                 536
Wasabi total volume:                        27467 BTC
Samourai total volume:                      171 BTC
Wasabi total mixed volume:                  2434 BTC
Samourai total mixed volume:                34 BTC
Wasabi anonset weighted volume mix score:   18851
Samourai anonset weighted volume mix score: 171
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
For example a transaction that has outputs of `1,2,,2,4,4,4` has a score of `2*2 + 4*3`.