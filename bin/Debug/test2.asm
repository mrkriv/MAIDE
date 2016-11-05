<?xml version="1.0" encoding="utf-8"?>
<project>
  <code>  myprog: start 0
      import printf
      ld x, #hello
      call printf
      ret

  hello: byte 'Hello, world!',10,0

  printsect: csect 0
      export printf
  printf:
      clear 	a
  printfloop:	+ldb 	a, 0[x]
      compb 	a, #0
      jeq 	printfend
      wd 	#0
      add 	x, #1
      jmp 	printfloop
  printfend:	ret






</code>
</project>