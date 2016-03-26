ld a, #48
ld b, #58

print:		; test comment
wd #0
incr a, b
jge print

ld a, #13
wd #0