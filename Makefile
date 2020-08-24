# Build zapws for Mono on OpenBSD

PROG=	zapws

SRCS=	zapws.cs
PROPS=	Properties/AssemblyInfo.cs

MAIN=	Program
APPCONFIG=	App.config
DOCFILE=	${PROG}.doc

SRCDIR= src
BINDIR=	bin

# Enumerate libraries used
LIBS=	libcmdline

# Which C# language version to compile with
LANG=	7.3

# Global C# defined symbol(s)
CSSYMBOL=	NET45

.include "mono.prog.mk"
