# Build libs for Mono on OpenBSD

LIB=	libcmdline.dll

SRCS=	CommandLineProcessor.cs Option.cs OptionEventArgs.cs
SRCS+=	Properties/AssemblyInfo.cs

DOCFILE=	libcmdline.doc

SRCDIR= ${LIB:R}
BINDIR=	bin
.poison empty (BINDIR)

# Which C# language version to compile with
LANG=	7.3

# Set compiler flags
.ifmake debug
OUT=	${BINDIR}/Debug/${LIB}
.else
OUT=	${BINDIR}/${LIB}
.endif

FLAGS=	-nologo -target:library -out:${OUT}
FLAGS+=	-langversion:\"${LANG}\"
DFLAGS=	-debug -optimize-

# Real location of the C# source files
CS=	${SRCS:S,^,$(SRCDIR)/,g}

all release: testobj
	csc ${FLAGS} ${CS}
	@echo '${LIB:R} -> ${.CURDIR}/${OUT}'

debug: testobj
	csc ${FLAGS} ${DFLAGS} ${CS}
	@echo '${LIB:R} -> ${.CURDIR}/${OUT}'

doc: testobj
.if defined(DOCFILE)
	csc ${FLAGS} -doc:${DOCFILE} ${CS}
	@echo '${LIB:R} -> ${.CURDIR}/${OUT}'
.else
	@echo 'No DOCFILE defined, nothing to do'
.endif

testobj:
.if !exists(${BINDIR})
	@echo 'No output directory! Run `make obj` before proceeding'
	@false
.endif

obj:
.if !exists(${BINDIR})
	mkdir -p ${BINDIR}/Debug
.endif

clean:
	rm -rf ${BINDIR}
	rm -f ${LIB} ${DOCFILE}
	@mkdir -p ${BINDIR}/Debug
