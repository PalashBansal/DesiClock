# DesiClock
A local style clock using Hindi(Devanagari) language

## How to Contribute

### 1. If you are a graphic designer-
Design Background/Foreground/Highlighter Images and put into "assets" folder
Tip: Application depends on the "transparency" property of images.(Multiple file formats like .png , .bmp and .gif supports this property.)

### 2. If you are a software programmer-
Open \DesiClock\DesiClock.sln to open project.
Folders "nonDotNET_artifacts" & "taken_from_linkedin" are not part of the .sln

For any bugs or new feature suggestions- Use the "Issues" tab.

Contents of files "\nonDotNET_artifacts\config_default.ini" and "\nonDotNET_artifacts\config_vocab_default.ini" should be same as the one created by executable.

Use 1PR<==>1Commit rule

Avoid sending PR containing unnecessary changes like whitespace addtion/deletion or code changes not effecting the final executable behavior(Except in case of PR solely dedicated to code refactoring)

Project highly depends on crafted Background("BackImage.png") and Foreground images("DefaultImageBg.png").
Specification-
Boundary of both images are overlapped in a typical clock. By default- 500x500 pixel round clocks.
Words not representing time takes color/pattern of back image.
Words representing time takes color/pattern of HighlighterImage.png

Application in actual stacks images in following order(transaxial view) also represented in embedded layer.png image down below-
1. Foreground image
2. HighlighterImageS
3. Background image
4. Form(Canvas)

**************************************************************************************
<img src="layers.png" align="middle" />
**************************************************************************************

Any improvements are highly appreciated ☺

SUPPORT:

If you liked the app and would like to grab me a coffee-

Bitcoin(BTC) - 331RPAL7LsAjL2BzUKDgHsixQWepMbfchX

Paypal - palash.bansal20@gmail.com