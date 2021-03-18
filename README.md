# aJS Native Compiler

A compiler for turning JavaScript code into **actual** native code, not a packaged node executable!

## What?

This project transpiles JavaScript into C#, which you can then compile as a native PC/Mac/Linux executable via .NET Core.

For example, see [ajs-test](test) which contains a compiled version of the following JavaScript FizzBuzz program:

```js
let n = 1
function runNextFizzbuzz () {
  let str = ''
  if (n % 3 === 0) {
    str = 'Fizz'
  }
  if (n % 5 === 0) {
    str = str + 'Buzz'
  }
  if (str === '') {
    str = n
  }
  console.log(str)
  n = n + 1
  if (n !== 101) {
    runNextFizzbuzz()
  }
}
runNextFizzbuzz()
```

The JS code is a little funky since aJS is missing support for loops at the moment, it's very early days. But it compiles and runs with identical output on node and native.

## Why?

Why not? 😉 Plus you don't have to have a multi-MB binary because it's got node/Chromium/etc. crammed into it

## Is it faster?

Absolutely not. Initially being faster was one of my pipe dreams, but the V8 team is incredibly talented and, as a result, node is remarkably fast. The above FizzBuzz demo runs about half as fast as node.

I believe the lack of speed is mostly due to the fat runtime that aJS needs to bundle your code with (not fat binary-size wise, but code/execution complexity wise) in order to emulate things like `undefined`, `NaN`, correct scoping, and strange operator rules (`==` vs `===`).
