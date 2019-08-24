# Wasabi vs Samourai coinjoin statistics

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
Samourai transaction count:                 1330
Wasabi total volume:                        13358 BTC
Samourai total volume:                      259 BTC
Wasabi total mixed volume:                  8402 BTC
Samourai total mixed volume:                259 BTC
Wasabi anonset weighted volume mix score:   308361
Samourai anonset weighted volume mix score: 1299

2019.8
Wasabi transaction count:                   444
Samourai transaction count:                 538
Wasabi total volume:                        27467 BTC
Samourai total volume:                      171 BTC
Wasabi total mixed volume:                  18851 BTC
Samourai total mixed volume:                171 BTC
Wasabi anonset weighted volume mix score:   417509
Samourai anonset weighted volume mix score: 859
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