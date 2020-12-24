module Main where

import Lib
import Data.Char

main :: IO ()
main = print result

s :: String
s = "Hello Word"

addThree :: Num a => a -> a
addThree x = x + 3
double :: Num a => a -> a
double x = x * 2

doubeThenAdd :: Num a => a -> a
doubeThenAdd = addThree . double

n = 8
-- result = doubeThenAdd n

composer :: (b -> c) -> (a -> b) -> a -> c
composer f g = f . g

dTA = composer addThree double

-- result = dTA n

-- writer
type Writer a = (a, String)

(>=>) :: (a -> Writer b) -> (b -> Writer c) -> (a -> Writer c)
m1 >=> m2 = \x -> 
    let (y, s1) = m1 x
        (z, s2) = m2 y
    in (z, s1 ++ s2)

return :: a -> Writer a
return x = (x, "")

upCase :: String -> Writer String
upCase s = (map toUpper s, "upCase; ")

toWords :: String -> Writer [String]
toWords s = (words s, "toWords; ")

process :: String -> Writer [String]
process = upCase >=> toWords

testString :: String
testString = "What a wonderful TeSt I got"

getLog :: Writer a -> String
getLog = snd

result = getLog (process testString)

-- partial functions https://bartoszmilewski.com/2014/12/23/kleisli-categories/
type ResultWithCheck a = (Bool, a)

check :: a -> ResultWithCheck a
check x = (True , x)

safeRoot :: (Ord a, Floating a) => a -> ResultWithCheck a
safeRoot x = 
    if x > 0 then (True, sqrt x)
    else (False, 0)

safeReciprocal :: (Eq a, Floating a) => a -> ResultWithCheck a
safeReciprocal x = 
    if x /= 0 then (True, 1/x)
    else (False , 0)

comb :: (a -> ResultWithCheck a) -> (a -> ResultWithCheck a) -> (a -> ResultWithCheck a)
comb f_outter f_inner = \x ->
    let (check, result) = f_inner x
    in 
        if check then f_inner x
        else f_outter result

-- result = comb safeReciprocal safeRoot (-1)

mult :: (Num a) => a -> a -> a -> a
mult x y z = x * y * z

mult3 = mult 3

-- result = mult3 3 3
