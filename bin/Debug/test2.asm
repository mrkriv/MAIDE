<?xml version="1.0" encoding="utf-8"?>
<project>
  <code>start:
	ld		x, 0
	call	printf
	ret

printf:
	clear	a
printfloop:
	ldb 	a, 0[x]
	compb	a, #0
	jeq		printfend
	wd		#0
	add		x, #1
	jmp		printfloop
printfend:
	ret</code>
</project>