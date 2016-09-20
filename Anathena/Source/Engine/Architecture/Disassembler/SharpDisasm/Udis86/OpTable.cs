namespace Ana.Source.Engine.Architecture.Disassembler.SharpDisasm.Udis86
{

    #region Enums

    public enum UdTableType
    {
        UD_TAB__OPC_TABLE,
        UD_TAB__OPC_SSE,
        UD_TAB__OPC_REG,
        UD_TAB__OPC_RM,
        UD_TAB__OPC_MOD,
        UD_TAB__OPC_MODE,
        UD_TAB__OPC_X87,
        UD_TAB__OPC_ASIZE,
        UD_TAB__OPC_OSIZE,
        UD_TAB__OPC_3DNOW,
        UD_TAB__OPC_VENDOR,
        UD_TAB__OPC_VEX,
        UD_TAB__OPC_VEX_W,
        UD_TAB__OPC_VEX_L
    }

    public enum UdMnemonicCode
    {
        UD_Iaaa,
        UD_Iaad,
        UD_Iaam,
        UD_Iaas,
        UD_Iadc,
        UD_Iadd,
        UD_Iaddpd,
        UD_Iaddps,
        UD_Iaddsd,
        UD_Iaddss,
        UD_Iaddsubpd,
        UD_Iaddsubps,
        UD_Iaesdec,
        UD_Iaesdeclast,
        UD_Iaesenc,
        UD_Iaesenclast,
        UD_Iaesimc,
        UD_Iaeskeygenassist,
        UD_Iand,
        UD_Iandnpd,
        UD_Iandnps,
        UD_Iandpd,
        UD_Iandps,
        UD_Iarpl,
        UD_Iblendpd,
        UD_Iblendps,
        UD_Iblendvpd,
        UD_Iblendvps,
        UD_Ibound,
        UD_Ibsf,
        UD_Ibsr,
        UD_Ibswap,
        UD_Ibt,
        UD_Ibtc,
        UD_Ibtr,
        UD_Ibts,
        UD_Icall,
        UD_Icbw,
        UD_Icdq,
        UD_Icdqe,
        UD_Iclc,
        UD_Icld,
        UD_Iclflush,
        UD_Iclgi,
        UD_Icli,
        UD_Iclts,
        UD_Icmc,
        UD_Icmova,
        UD_Icmovae,
        UD_Icmovb,
        UD_Icmovbe,
        UD_Icmovg,
        UD_Icmovge,
        UD_Icmovl,
        UD_Icmovle,
        UD_Icmovno,
        UD_Icmovnp,
        UD_Icmovns,
        UD_Icmovnz,
        UD_Icmovo,
        UD_Icmovp,
        UD_Icmovs,
        UD_Icmovz,
        UD_Icmp,
        UD_Icmppd,
        UD_Icmpps,
        UD_Icmpsb,
        UD_Icmpsd,
        UD_Icmpsq,
        UD_Icmpss,
        UD_Icmpsw,
        UD_Icmpxchg,
        UD_Icmpxchg16b,
        UD_Icmpxchg8b,
        UD_Icomisd,
        UD_Icomiss,
        UD_Icpuid,
        UD_Icqo,
        UD_Icrc32,
        UD_Icvtdq2pd,
        UD_Icvtdq2ps,
        UD_Icvtpd2dq,
        UD_Icvtpd2pi,
        UD_Icvtpd2ps,
        UD_Icvtpi2pd,
        UD_Icvtpi2ps,
        UD_Icvtps2dq,
        UD_Icvtps2pd,
        UD_Icvtps2pi,
        UD_Icvtsd2si,
        UD_Icvtsd2ss,
        UD_Icvtsi2sd,
        UD_Icvtsi2ss,
        UD_Icvtss2sd,
        UD_Icvtss2si,
        UD_Icvttpd2dq,
        UD_Icvttpd2pi,
        UD_Icvttps2dq,
        UD_Icvttps2pi,
        UD_Icvttsd2si,
        UD_Icvttss2si,
        UD_Icwd,
        UD_Icwde,
        UD_Idaa,
        UD_Idas,
        UD_Idec,
        UD_Idiv,
        UD_Idivpd,
        UD_Idivps,
        UD_Idivsd,
        UD_Idivss,
        UD_Idppd,
        UD_Idpps,
        UD_Iemms,
        UD_Ienter,
        UD_Iextractps,
        UD_If2xm1,
        UD_Ifabs,
        UD_Ifadd,
        UD_Ifaddp,
        UD_Ifbld,
        UD_Ifbstp,
        UD_Ifchs,
        UD_Ifclex,
        UD_Ifcmovb,
        UD_Ifcmovbe,
        UD_Ifcmove,
        UD_Ifcmovnb,
        UD_Ifcmovnbe,
        UD_Ifcmovne,
        UD_Ifcmovnu,
        UD_Ifcmovu,
        UD_Ifcom,
        UD_Ifcom2,
        UD_Ifcomi,
        UD_Ifcomip,
        UD_Ifcomp,
        UD_Ifcomp3,
        UD_Ifcomp5,
        UD_Ifcompp,
        UD_Ifcos,
        UD_Ifdecstp,
        UD_Ifdiv,
        UD_Ifdivp,
        UD_Ifdivr,
        UD_Ifdivrp,
        UD_Ifemms,
        UD_Iffree,
        UD_Iffreep,
        UD_Ifiadd,
        UD_Ificom,
        UD_Ificomp,
        UD_Ifidiv,
        UD_Ifidivr,
        UD_Ifild,
        UD_Ifimul,
        UD_Ifincstp,
        UD_Ifist,
        UD_Ifistp,
        UD_Ifisttp,
        UD_Ifisub,
        UD_Ifisubr,
        UD_Ifld,
        UD_Ifld1,
        UD_Ifldcw,
        UD_Ifldenv,
        UD_Ifldl2e,
        UD_Ifldl2t,
        UD_Ifldlg2,
        UD_Ifldln2,
        UD_Ifldpi,
        UD_Ifldz,
        UD_Ifmul,
        UD_Ifmulp,
        UD_Ifndisi,
        UD_Ifneni,
        UD_Ifninit,
        UD_Ifnop,
        UD_Ifnsave,
        UD_Ifnsetpm,
        UD_Ifnstcw,
        UD_Ifnstenv,
        UD_Ifnstsw,
        UD_Ifpatan,
        UD_Ifprem,
        UD_Ifprem1,
        UD_Ifptan,
        UD_Ifrndint,
        UD_Ifrstor,
        UD_Ifrstpm,
        UD_Ifscale,
        UD_Ifsin,
        UD_Ifsincos,
        UD_Ifsqrt,
        UD_Ifst,
        UD_Ifstp,
        UD_Ifstp1,
        UD_Ifstp8,
        UD_Ifstp9,
        UD_Ifsub,
        UD_Ifsubp,
        UD_Ifsubr,
        UD_Ifsubrp,
        UD_Iftst,
        UD_Ifucom,
        UD_Ifucomi,
        UD_Ifucomip,
        UD_Ifucomp,
        UD_Ifucompp,
        UD_Ifxam,
        UD_Ifxch,
        UD_Ifxch4,
        UD_Ifxch7,
        UD_Ifxrstor,
        UD_Ifxsave,
        UD_Ifxtract,
        UD_Ifyl2x,
        UD_Ifyl2xp1,
        UD_Igetsec,
        UD_Ihaddpd,
        UD_Ihaddps,
        UD_Ihlt,
        UD_Ihsubpd,
        UD_Ihsubps,
        UD_Iidiv,
        UD_Iimul,
        UD_Iin,
        UD_Iinc,
        UD_Iinsb,
        UD_Iinsd,
        UD_Iinsertps,
        UD_Iinsw,
        UD_Iint,
        UD_Iint1,
        UD_Iint3,
        UD_Iinto,
        UD_Iinvd,
        UD_Iinvept,
        UD_Iinvlpg,
        UD_Iinvlpga,
        UD_Iinvvpid,
        UD_Iiretd,
        UD_Iiretq,
        UD_Iiretw,
        UD_Ija,
        UD_Ijae,
        UD_Ijb,
        UD_Ijbe,
        UD_Ijcxz,
        UD_Ijecxz,
        UD_Ijg,
        UD_Ijge,
        UD_Ijl,
        UD_Ijle,
        UD_Ijmp,
        UD_Ijno,
        UD_Ijnp,
        UD_Ijns,
        UD_Ijnz,
        UD_Ijo,
        UD_Ijp,
        UD_Ijrcxz,
        UD_Ijs,
        UD_Ijz,
        UD_Ilahf,
        UD_Ilar,
        UD_Ilddqu,
        UD_Ildmxcsr,
        UD_Ilds,
        UD_Ilea,
        UD_Ileave,
        UD_Iles,
        UD_Ilfence,
        UD_Ilfs,
        UD_Ilgdt,
        UD_Ilgs,
        UD_Ilidt,
        UD_Illdt,
        UD_Ilmsw,
        UD_Ilock,
        UD_Ilodsb,
        UD_Ilodsd,
        UD_Ilodsq,
        UD_Ilodsw,
        UD_Iloop,
        UD_Iloope,
        UD_Iloopne,
        UD_Ilsl,
        UD_Ilss,
        UD_Iltr,
        UD_Imaskmovdqu,
        UD_Imaskmovq,
        UD_Imaxpd,
        UD_Imaxps,
        UD_Imaxsd,
        UD_Imaxss,
        UD_Imfence,
        UD_Iminpd,
        UD_Iminps,
        UD_Iminsd,
        UD_Iminss,
        UD_Imonitor,
        UD_Imontmul,
        UD_Imov,
        UD_Imovapd,
        UD_Imovaps,
        UD_Imovbe,
        UD_Imovd,
        UD_Imovddup,
        UD_Imovdq2q,
        UD_Imovdqa,
        UD_Imovdqu,
        UD_Imovhlps,
        UD_Imovhpd,
        UD_Imovhps,
        UD_Imovlhps,
        UD_Imovlpd,
        UD_Imovlps,
        UD_Imovmskpd,
        UD_Imovmskps,
        UD_Imovntdq,
        UD_Imovntdqa,
        UD_Imovnti,
        UD_Imovntpd,
        UD_Imovntps,
        UD_Imovntq,
        UD_Imovq,
        UD_Imovq2dq,
        UD_Imovsb,
        UD_Imovsd,
        UD_Imovshdup,
        UD_Imovsldup,
        UD_Imovsq,
        UD_Imovss,
        UD_Imovsw,
        UD_Imovsx,
        UD_Imovsxd,
        UD_Imovupd,
        UD_Imovups,
        UD_Imovzx,
        UD_Impsadbw,
        UD_Imul,
        UD_Imulpd,
        UD_Imulps,
        UD_Imulsd,
        UD_Imulss,
        UD_Imwait,
        UD_Ineg,
        UD_Inop,
        UD_Inot,
        UD_Ior,
        UD_Iorpd,
        UD_Iorps,
        UD_Iout,
        UD_Ioutsb,
        UD_Ioutsd,
        UD_Ioutsw,
        UD_Ipabsb,
        UD_Ipabsd,
        UD_Ipabsw,
        UD_Ipackssdw,
        UD_Ipacksswb,
        UD_Ipackusdw,
        UD_Ipackuswb,
        UD_Ipaddb,
        UD_Ipaddd,
        UD_Ipaddq,
        UD_Ipaddsb,
        UD_Ipaddsw,
        UD_Ipaddusb,
        UD_Ipaddusw,
        UD_Ipaddw,
        UD_Ipalignr,
        UD_Ipand,
        UD_Ipandn,
        UD_Ipavgb,
        UD_Ipavgusb,
        UD_Ipavgw,
        UD_Ipblendvb,
        UD_Ipblendw,
        UD_Ipclmulqdq,
        UD_Ipcmpeqb,
        UD_Ipcmpeqd,
        UD_Ipcmpeqq,
        UD_Ipcmpeqw,
        UD_Ipcmpestri,
        UD_Ipcmpestrm,
        UD_Ipcmpgtb,
        UD_Ipcmpgtd,
        UD_Ipcmpgtq,
        UD_Ipcmpgtw,
        UD_Ipcmpistri,
        UD_Ipcmpistrm,
        UD_Ipextrb,
        UD_Ipextrd,
        UD_Ipextrq,
        UD_Ipextrw,
        UD_Ipf2id,
        UD_Ipf2iw,
        UD_Ipfacc,
        UD_Ipfadd,
        UD_Ipfcmpeq,
        UD_Ipfcmpge,
        UD_Ipfcmpgt,
        UD_Ipfmax,
        UD_Ipfmin,
        UD_Ipfmul,
        UD_Ipfnacc,
        UD_Ipfpnacc,
        UD_Ipfrcp,
        UD_Ipfrcpit1,
        UD_Ipfrcpit2,
        UD_Ipfrsqit1,
        UD_Ipfrsqrt,
        UD_Ipfsub,
        UD_Ipfsubr,
        UD_Iphaddd,
        UD_Iphaddsw,
        UD_Iphaddw,
        UD_Iphminposuw,
        UD_Iphsubd,
        UD_Iphsubsw,
        UD_Iphsubw,
        UD_Ipi2fd,
        UD_Ipi2fw,
        UD_Ipinsrb,
        UD_Ipinsrd,
        UD_Ipinsrq,
        UD_Ipinsrw,
        UD_Ipmaddubsw,
        UD_Ipmaddwd,
        UD_Ipmaxsb,
        UD_Ipmaxsd,
        UD_Ipmaxsw,
        UD_Ipmaxub,
        UD_Ipmaxud,
        UD_Ipmaxuw,
        UD_Ipminsb,
        UD_Ipminsd,
        UD_Ipminsw,
        UD_Ipminub,
        UD_Ipminud,
        UD_Ipminuw,
        UD_Ipmovmskb,
        UD_Ipmovsxbd,
        UD_Ipmovsxbq,
        UD_Ipmovsxbw,
        UD_Ipmovsxdq,
        UD_Ipmovsxwd,
        UD_Ipmovsxwq,
        UD_Ipmovzxbd,
        UD_Ipmovzxbq,
        UD_Ipmovzxbw,
        UD_Ipmovzxdq,
        UD_Ipmovzxwd,
        UD_Ipmovzxwq,
        UD_Ipmuldq,
        UD_Ipmulhrsw,
        UD_Ipmulhrw,
        UD_Ipmulhuw,
        UD_Ipmulhw,
        UD_Ipmulld,
        UD_Ipmullw,
        UD_Ipmuludq,
        UD_Ipop,
        UD_Ipopa,
        UD_Ipopad,
        UD_Ipopcnt,
        UD_Ipopfd,
        UD_Ipopfq,
        UD_Ipopfw,
        UD_Ipor,
        UD_Iprefetch,
        UD_Iprefetchnta,
        UD_Iprefetcht0,
        UD_Iprefetcht1,
        UD_Iprefetcht2,
        UD_Ipsadbw,
        UD_Ipshufb,
        UD_Ipshufd,
        UD_Ipshufhw,
        UD_Ipshuflw,
        UD_Ipshufw,
        UD_Ipsignb,
        UD_Ipsignd,
        UD_Ipsignw,
        UD_Ipslld,
        UD_Ipslldq,
        UD_Ipsllq,
        UD_Ipsllw,
        UD_Ipsrad,
        UD_Ipsraw,
        UD_Ipsrld,
        UD_Ipsrldq,
        UD_Ipsrlq,
        UD_Ipsrlw,
        UD_Ipsubb,
        UD_Ipsubd,
        UD_Ipsubq,
        UD_Ipsubsb,
        UD_Ipsubsw,
        UD_Ipsubusb,
        UD_Ipsubusw,
        UD_Ipsubw,
        UD_Ipswapd,
        UD_Iptest,
        UD_Ipunpckhbw,
        UD_Ipunpckhdq,
        UD_Ipunpckhqdq,
        UD_Ipunpckhwd,
        UD_Ipunpcklbw,
        UD_Ipunpckldq,
        UD_Ipunpcklqdq,
        UD_Ipunpcklwd,
        UD_Ipush,
        UD_Ipusha,
        UD_Ipushad,
        UD_Ipushfd,
        UD_Ipushfq,
        UD_Ipushfw,
        UD_Ipxor,
        UD_Ircl,
        UD_Ircpps,
        UD_Ircpss,
        UD_Ircr,
        UD_Irdmsr,
        UD_Irdpmc,
        UD_Irdrand,
        UD_Irdtsc,
        UD_Irdtscp,
        UD_Irep,
        UD_Irepne,
        UD_Iret,
        UD_Iretf,
        UD_Irol,
        UD_Iror,
        UD_Iroundpd,
        UD_Iroundps,
        UD_Iroundsd,
        UD_Iroundss,
        UD_Irsm,
        UD_Irsqrtps,
        UD_Irsqrtss,
        UD_Isahf,
        UD_Isalc,
        UD_Isar,
        UD_Isbb,
        UD_Iscasb,
        UD_Iscasd,
        UD_Iscasq,
        UD_Iscasw,
        UD_Iseta,
        UD_Isetae,
        UD_Isetb,
        UD_Isetbe,
        UD_Isetg,
        UD_Isetge,
        UD_Isetl,
        UD_Isetle,
        UD_Isetno,
        UD_Isetnp,
        UD_Isetns,
        UD_Isetnz,
        UD_Iseto,
        UD_Isetp,
        UD_Isets,
        UD_Isetz,
        UD_Isfence,
        UD_Isgdt,
        UD_Ishl,
        UD_Ishld,
        UD_Ishr,
        UD_Ishrd,
        UD_Ishufpd,
        UD_Ishufps,
        UD_Isidt,
        UD_Iskinit,
        UD_Isldt,
        UD_Ismsw,
        UD_Isqrtpd,
        UD_Isqrtps,
        UD_Isqrtsd,
        UD_Isqrtss,
        UD_Istc,
        UD_Istd,
        UD_Istgi,
        UD_Isti,
        UD_Istmxcsr,
        UD_Istosb,
        UD_Istosd,
        UD_Istosq,
        UD_Istosw,
        UD_Istr,
        UD_Isub,
        UD_Isubpd,
        UD_Isubps,
        UD_Isubsd,
        UD_Isubss,
        UD_Iswapgs,
        UD_Isyscall,
        UD_Isysenter,
        UD_Isysexit,
        UD_Isysret,
        UD_Itest,
        UD_Iucomisd,
        UD_Iucomiss,
        UD_Iud2,
        UD_Iunpckhpd,
        UD_Iunpckhps,
        UD_Iunpcklpd,
        UD_Iunpcklps,
        UD_Ivaddpd,
        UD_Ivaddps,
        UD_Ivaddsd,
        UD_Ivaddss,
        UD_Ivaddsubpd,
        UD_Ivaddsubps,
        UD_Ivaesdec,
        UD_Ivaesdeclast,
        UD_Ivaesenc,
        UD_Ivaesenclast,
        UD_Ivaesimc,
        UD_Ivaeskeygenassist,
        UD_Ivandnpd,
        UD_Ivandnps,
        UD_Ivandpd,
        UD_Ivandps,
        UD_Ivblendpd,
        UD_Ivblendps,
        UD_Ivblendvpd,
        UD_Ivblendvps,
        UD_Ivbroadcastsd,
        UD_Ivbroadcastss,
        UD_Ivcmppd,
        UD_Ivcmpps,
        UD_Ivcmpsd,
        UD_Ivcmpss,
        UD_Ivcomisd,
        UD_Ivcomiss,
        UD_Ivcvtdq2pd,
        UD_Ivcvtdq2ps,
        UD_Ivcvtpd2dq,
        UD_Ivcvtpd2ps,
        UD_Ivcvtps2dq,
        UD_Ivcvtps2pd,
        UD_Ivcvtsd2si,
        UD_Ivcvtsd2ss,
        UD_Ivcvtsi2sd,
        UD_Ivcvtsi2ss,
        UD_Ivcvtss2sd,
        UD_Ivcvtss2si,
        UD_Ivcvttpd2dq,
        UD_Ivcvttps2dq,
        UD_Ivcvttsd2si,
        UD_Ivcvttss2si,
        UD_Ivdivpd,
        UD_Ivdivps,
        UD_Ivdivsd,
        UD_Ivdivss,
        UD_Ivdppd,
        UD_Ivdpps,
        UD_Iverr,
        UD_Iverw,
        UD_Ivextractf128,
        UD_Ivextractps,
        UD_Ivhaddpd,
        UD_Ivhaddps,
        UD_Ivhsubpd,
        UD_Ivhsubps,
        UD_Ivinsertf128,
        UD_Ivinsertps,
        UD_Ivlddqu,
        UD_Ivmaskmovdqu,
        UD_Ivmaskmovpd,
        UD_Ivmaskmovps,
        UD_Ivmaxpd,
        UD_Ivmaxps,
        UD_Ivmaxsd,
        UD_Ivmaxss,
        UD_Ivmcall,
        UD_Ivmclear,
        UD_Ivminpd,
        UD_Ivminps,
        UD_Ivminsd,
        UD_Ivminss,
        UD_Ivmlaunch,
        UD_Ivmload,
        UD_Ivmmcall,
        UD_Ivmovapd,
        UD_Ivmovaps,
        UD_Ivmovd,
        UD_Ivmovddup,
        UD_Ivmovdqa,
        UD_Ivmovdqu,
        UD_Ivmovhlps,
        UD_Ivmovhpd,
        UD_Ivmovhps,
        UD_Ivmovlhps,
        UD_Ivmovlpd,
        UD_Ivmovlps,
        UD_Ivmovmskpd,
        UD_Ivmovmskps,
        UD_Ivmovntdq,
        UD_Ivmovntdqa,
        UD_Ivmovntpd,
        UD_Ivmovntps,
        UD_Ivmovq,
        UD_Ivmovsd,
        UD_Ivmovshdup,
        UD_Ivmovsldup,
        UD_Ivmovss,
        UD_Ivmovupd,
        UD_Ivmovups,
        UD_Ivmpsadbw,
        UD_Ivmptrld,
        UD_Ivmptrst,
        UD_Ivmread,
        UD_Ivmresume,
        UD_Ivmrun,
        UD_Ivmsave,
        UD_Ivmulpd,
        UD_Ivmulps,
        UD_Ivmulsd,
        UD_Ivmulss,
        UD_Ivmwrite,
        UD_Ivmxoff,
        UD_Ivmxon,
        UD_Ivorpd,
        UD_Ivorps,
        UD_Ivpabsb,
        UD_Ivpabsd,
        UD_Ivpabsw,
        UD_Ivpackssdw,
        UD_Ivpacksswb,
        UD_Ivpackusdw,
        UD_Ivpackuswb,
        UD_Ivpaddb,
        UD_Ivpaddd,
        UD_Ivpaddq,
        UD_Ivpaddsb,
        UD_Ivpaddsw,
        UD_Ivpaddusb,
        UD_Ivpaddusw,
        UD_Ivpaddw,
        UD_Ivpalignr,
        UD_Ivpand,
        UD_Ivpandn,
        UD_Ivpavgb,
        UD_Ivpavgw,
        UD_Ivpblendvb,
        UD_Ivpblendw,
        UD_Ivpclmulqdq,
        UD_Ivpcmpeqb,
        UD_Ivpcmpeqd,
        UD_Ivpcmpeqq,
        UD_Ivpcmpeqw,
        UD_Ivpcmpestri,
        UD_Ivpcmpestrm,
        UD_Ivpcmpgtb,
        UD_Ivpcmpgtd,
        UD_Ivpcmpgtq,
        UD_Ivpcmpgtw,
        UD_Ivpcmpistri,
        UD_Ivpcmpistrm,
        UD_Ivperm2f128,
        UD_Ivpermilpd,
        UD_Ivpermilps,
        UD_Ivpextrb,
        UD_Ivpextrd,
        UD_Ivpextrq,
        UD_Ivpextrw,
        UD_Ivphaddd,
        UD_Ivphaddsw,
        UD_Ivphaddw,
        UD_Ivphminposuw,
        UD_Ivphsubd,
        UD_Ivphsubsw,
        UD_Ivphsubw,
        UD_Ivpinsrb,
        UD_Ivpinsrd,
        UD_Ivpinsrq,
        UD_Ivpinsrw,
        UD_Ivpmaddubsw,
        UD_Ivpmaddwd,
        UD_Ivpmaxsb,
        UD_Ivpmaxsd,
        UD_Ivpmaxsw,
        UD_Ivpmaxub,
        UD_Ivpmaxud,
        UD_Ivpmaxuw,
        UD_Ivpminsb,
        UD_Ivpminsd,
        UD_Ivpminsw,
        UD_Ivpminub,
        UD_Ivpminud,
        UD_Ivpminuw,
        UD_Ivpmovmskb,
        UD_Ivpmovsxbd,
        UD_Ivpmovsxbq,
        UD_Ivpmovsxbw,
        UD_Ivpmovsxwd,
        UD_Ivpmovsxwq,
        UD_Ivpmovzxbd,
        UD_Ivpmovzxbq,
        UD_Ivpmovzxbw,
        UD_Ivpmovzxdq,
        UD_Ivpmovzxwd,
        UD_Ivpmovzxwq,
        UD_Ivpmuldq,
        UD_Ivpmulhrsw,
        UD_Ivpmulhuw,
        UD_Ivpmulhw,
        UD_Ivpmulld,
        UD_Ivpmullw,
        UD_Ivpor,
        UD_Ivpsadbw,
        UD_Ivpshufb,
        UD_Ivpshufd,
        UD_Ivpshufhw,
        UD_Ivpshuflw,
        UD_Ivpsignb,
        UD_Ivpsignd,
        UD_Ivpsignw,
        UD_Ivpslld,
        UD_Ivpslldq,
        UD_Ivpsllq,
        UD_Ivpsllw,
        UD_Ivpsrad,
        UD_Ivpsraw,
        UD_Ivpsrld,
        UD_Ivpsrldq,
        UD_Ivpsrlq,
        UD_Ivpsrlw,
        UD_Ivpsubb,
        UD_Ivpsubd,
        UD_Ivpsubq,
        UD_Ivpsubsb,
        UD_Ivpsubsw,
        UD_Ivpsubusb,
        UD_Ivpsubusw,
        UD_Ivpsubw,
        UD_Ivptest,
        UD_Ivpunpckhbw,
        UD_Ivpunpckhdq,
        UD_Ivpunpckhqdq,
        UD_Ivpunpckhwd,
        UD_Ivpunpcklbw,
        UD_Ivpunpckldq,
        UD_Ivpunpcklqdq,
        UD_Ivpunpcklwd,
        UD_Ivpxor,
        UD_Ivrcpps,
        UD_Ivrcpss,
        UD_Ivroundpd,
        UD_Ivroundps,
        UD_Ivroundsd,
        UD_Ivroundss,
        UD_Ivrsqrtps,
        UD_Ivrsqrtss,
        UD_Ivshufpd,
        UD_Ivshufps,
        UD_Ivsqrtpd,
        UD_Ivsqrtps,
        UD_Ivsqrtsd,
        UD_Ivsqrtss,
        UD_Ivstmxcsr,
        UD_Ivsubpd,
        UD_Ivsubps,
        UD_Ivsubsd,
        UD_Ivsubss,
        UD_Ivtestpd,
        UD_Ivtestps,
        UD_Ivucomisd,
        UD_Ivucomiss,
        UD_Ivunpckhpd,
        UD_Ivunpckhps,
        UD_Ivunpcklpd,
        UD_Ivunpcklps,
        UD_Ivxorpd,
        UD_Ivxorps,
        UD_Ivzeroall,
        UD_Ivzeroupper,
        UD_Iwait,
        UD_Iwbinvd,
        UD_Iwrmsr,
        UD_Ixadd,
        UD_Ixchg,
        UD_Ixcryptcbc,
        UD_Ixcryptcfb,
        UD_Ixcryptctr,
        UD_Ixcryptecb,
        UD_Ixcryptofb,
        UD_Ixgetbv,
        UD_Ixlatb,
        UD_Ixor,
        UD_Ixorpd,
        UD_Ixorps,
        UD_Ixrstor,
        UD_Ixsave,
        UD_Ixsetbv,
        UD_Ixsha1,
        UD_Ixsha256,
        UD_Ixstore,
        UD_Iinvalid,
        UD_I3dnow,
        UD_Inone,
        UD_Idb,
        UD_Ipause,
        UD_MAX_MNEMONIC_CODE
    }

    #endregion

    internal static class InstructionTables
    {
        #region Lookup Tables
        public const int INVALID = 0;

        internal static readonly ushort[] ud_itab__0 = {
          /* 00 */          15,          16,          17,          18,
          /* 04 */          19,          20,    0x8000|1,    0x8000|2,
          /* 08 */         964,         965,         966,         967,
          /* 0c */         968,         969,    0x8000|3,    0x8000|4,
          /* 10 */           5,           6,           7,           8,
          /* 14 */           9,          10,  0x8000|284,  0x8000|285,
          /* 18 */        1336,        1337,        1338,        1339,
          /* 1c */        1340,        1341,  0x8000|286,  0x8000|287,
          /* 20 */          49,          50,          51,          52,
          /* 24 */          53,          54,     INVALID,  0x8000|288,
          /* 28 */        1407,        1408,        1409,        1410,
          /* 2c */        1411,        1412,     INVALID,  0x8000|289,
          /* 30 */        1487,        1488,        1489,        1490,
          /* 34 */        1491,        1492,     INVALID,  0x8000|290,
          /* 38 */         100,         101,         102,         103,
          /* 3c */         104,         105,     INVALID,  0x8000|291,
          /* 40 */         699,         700,         701,         702,
          /* 44 */         703,         704,         705,         706,
          /* 48 */         175,         176,         177,         178,
          /* 4c */         179,         180,         181,         182,
          /* 50 */        1246,        1247,        1248,        1249,
          /* 54 */        1250,        1251,        1252,        1253,
          /* 58 */        1101,        1102,        1103,        1104,
          /* 5c */        1105,        1106,        1107,        1108,
          /* 60 */  0x8000|292,  0x8000|295,  0x8000|298,  0x8000|299,
          /* 64 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 68 */        1254,         697,        1256,         698,
          /* 6c */         709,  0x8000|300,         982,  0x8000|301,
          /* 70 */         726,         728,         730,         732,
          /* 74 */         734,         736,         738,         740,
          /* 78 */         742,         744,         746,         748,
          /* 7c */         750,         752,         754,         756,
          /* 80 */  0x8000|302,  0x8000|303,  0x8000|304,  0x8000|313,
          /* 84 */        1433,        1434,        1475,        1476,
          /* 88 */         828,         829,         830,         831,
          /* 8c */         832,         770,         833,  0x8000|314,
          /* 90 */        1477,        1478,        1479,        1480,
          /* 94 */        1481,        1482,        1483,        1484,
          /* 98 */  0x8000|315,  0x8000|316,  0x8000|317,        1470,
          /* 9c */  0x8000|318,  0x8000|322,        1310,         766,
          /* a0 */         834,         835,         836,         837,
          /* a4 */         922,  0x8000|326,         114,  0x8000|327,
          /* a8 */        1435,        1436,        1402,  0x8000|328,
          /* ac */         790,  0x8000|329,        1346,  0x8000|330,
          /* b0 */         838,         839,         840,         841,
          /* b4 */         842,         843,         844,         845,
          /* b8 */         846,         847,         848,         849,
          /* bc */         850,         851,         852,         853,
          /* c0 */  0x8000|331,  0x8000|332,        1301,        1302,
          /* c4 */  0x8000|333,  0x8000|403,  0x8000|405,  0x8000|406,
          /* c8 */         200,         776,        1303,        1304,
          /* cc */         713,         714,  0x8000|407,  0x8000|408,
          /* d0 */  0x8000|409,  0x8000|410,  0x8000|411,  0x8000|412,
          /* d4 */  0x8000|413,  0x8000|414,  0x8000|415,        1486,
          /* d8 */  0x8000|416,  0x8000|419,  0x8000|422,  0x8000|425,
          /* dc */  0x8000|428,  0x8000|431,  0x8000|434,  0x8000|437,
          /* e0 */         794,         795,         796,  0x8000|440,
          /* e4 */         690,         691,         978,         979,
          /* e8 */          72,         763,  0x8000|441,         765,
          /* ec */         692,         693,         980,         981,
          /* f0 */         789,         712,        1299,        1300,
          /* f4 */         687,          83,  0x8000|442,  0x8000|443,
          /* f8 */          77,        1395,          81,        1398,
          /* fc */          78,        1396,  0x8000|444,  0x8000|445,
        };

        internal static readonly ushort[] ud_itab__1 = {
          /* 00 */        1240,     INVALID,
        };

        internal static readonly ushort[] ud_itab__2 = {
          /* 00 */        1096,     INVALID,
        };

        internal static readonly ushort[] ud_itab__3 = {
          /* 00 */        1241,     INVALID,
        };

        internal static readonly ushort[] ud_itab__4 = {
          /* 00 */    0x8000|5,    0x8000|6,         767,         797,
          /* 04 */     INVALID,        1426,          82,        1431,
          /* 08 */         716,        1471,     INVALID,        1444,
          /* 0c */     INVALID,   0x8000|27,         430,   0x8000|28,
          /* 10 */   0x8000|29,   0x8000|30,   0x8000|31,   0x8000|34,
          /* 14 */   0x8000|35,   0x8000|36,   0x8000|37,   0x8000|40,
          /* 18 */   0x8000|41,         955,         956,         957,
          /* 1c */         958,         959,         960,         961,
          /* 20 */         854,         855,         856,         857,
          /* 24 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 28 */   0x8000|42,   0x8000|43,   0x8000|44,   0x8000|45,
          /* 2c */   0x8000|46,   0x8000|47,   0x8000|48,   0x8000|49,
          /* 30 */        1472,        1297,        1295,        1296,
          /* 34 */   0x8000|50,   0x8000|52,     INVALID,        1514,
          /* 38 */   0x8000|54,     INVALID,  0x8000|116,     INVALID,
          /* 3c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 40 */          84,          85,          86,          87,
          /* 44 */          88,          89,          90,          91,
          /* 48 */          92,          93,          94,          95,
          /* 4c */          96,          97,          98,          99,
          /* 50 */  0x8000|143,  0x8000|144,  0x8000|145,  0x8000|146,
          /* 54 */  0x8000|147,  0x8000|148,  0x8000|149,  0x8000|150,
          /* 58 */  0x8000|151,  0x8000|152,  0x8000|153,  0x8000|154,
          /* 5c */  0x8000|155,  0x8000|156,  0x8000|157,  0x8000|158,
          /* 60 */  0x8000|159,  0x8000|160,  0x8000|161,  0x8000|162,
          /* 64 */  0x8000|163,  0x8000|164,  0x8000|165,  0x8000|166,
          /* 68 */  0x8000|167,  0x8000|168,  0x8000|169,  0x8000|170,
          /* 6c */  0x8000|171,  0x8000|172,  0x8000|173,  0x8000|176,
          /* 70 */  0x8000|177,  0x8000|178,  0x8000|182,  0x8000|186,
          /* 74 */  0x8000|191,  0x8000|192,  0x8000|193,         199,
          /* 78 */  0x8000|194,  0x8000|195,     INVALID,     INVALID,
          /* 7c */  0x8000|196,  0x8000|197,  0x8000|198,  0x8000|201,
          /* 80 */         727,         729,         731,         733,
          /* 84 */         735,         737,         739,         741,
          /* 88 */         743,         745,         747,         749,
          /* 8c */         751,         753,         755,         757,
          /* 90 */        1350,        1351,        1352,        1353,
          /* 94 */        1354,        1355,        1356,        1357,
          /* 98 */        1358,        1359,        1360,        1361,
          /* 9c */        1362,        1363,        1364,        1365,
          /* a0 */        1245,        1100,         131,        1670,
          /* a4 */        1375,        1376,  0x8000|202,  0x8000|207,
          /* a8 */        1244,        1099,        1305,        1675,
          /* ac */        1377,        1378,  0x8000|215,         694,
          /* b0 */         122,         123,         775,        1673,
          /* b4 */         772,         773,         940,         941,
          /* b8 */  0x8000|221,     INVALID,  0x8000|222,        1671,
          /* bc */        1659,        1660,         930,         931,
          /* c0 */        1473,        1474,  0x8000|223,         904,
          /* c4 */  0x8000|224,  0x8000|225,  0x8000|226,  0x8000|227,
          /* c8 */        1661,        1662,        1663,        1664,
          /* cc */        1665,        1666,        1667,        1668,
          /* d0 */  0x8000|236,  0x8000|237,  0x8000|238,  0x8000|239,
          /* d4 */  0x8000|240,  0x8000|241,  0x8000|242,  0x8000|243,
          /* d8 */  0x8000|244,  0x8000|245,  0x8000|246,  0x8000|247,
          /* dc */  0x8000|248,  0x8000|249,  0x8000|250,  0x8000|251,
          /* e0 */  0x8000|252,  0x8000|253,  0x8000|254,  0x8000|255,
          /* e4 */  0x8000|256,  0x8000|257,  0x8000|258,  0x8000|259,
          /* e8 */  0x8000|260,  0x8000|261,  0x8000|262,  0x8000|263,
          /* ec */  0x8000|264,  0x8000|265,  0x8000|266,  0x8000|267,
          /* f0 */  0x8000|268,  0x8000|269,  0x8000|270,  0x8000|271,
          /* f4 */  0x8000|272,  0x8000|273,  0x8000|274,  0x8000|275,
          /* f8 */  0x8000|277,  0x8000|278,  0x8000|279,  0x8000|280,
          /* fc */  0x8000|281,  0x8000|282,  0x8000|283,     INVALID,
        };

        internal static readonly ushort[] ud_itab__5 = {
          /* 00 */        1384,        1406,         786,         798,
          /* 04 */        1453,        1454,     INVALID,     INVALID,
        };

        internal static readonly ushort[] ud_itab__6 = {
          /* 00 */    0x8000|7,    0x8000|8,
        };

        internal static readonly ushort[] ud_itab__7 = {
          /* 00 */        1374,        1383,         785,         774,
          /* 04 */        1385,     INVALID,         787,         719,
        };

        internal static readonly ushort[] ud_itab__8 = {
          /* 00 */    0x8000|9,   0x8000|14,   0x8000|15,   0x8000|16,
          /* 04 */        1386,     INVALID,         788,   0x8000|25,
        };

        internal static readonly ushort[] ud_itab__9 = {
          /* 00 */     INVALID,   0x8000|10,   0x8000|11,   0x8000|12,
          /* 04 */   0x8000|13,     INVALID,     INVALID,     INVALID,
        };

        internal static readonly ushort[] ud_itab__10 = {
          /* 00 */     INVALID,        1455,     INVALID,
        };

        internal static readonly ushort[] ud_itab__11 = {
          /* 00 */     INVALID,        1461,     INVALID,
        };

        internal static readonly ushort[] ud_itab__12 = {
          /* 00 */     INVALID,        1462,     INVALID,
        };

        internal static readonly ushort[] ud_itab__13 = {
          /* 00 */     INVALID,        1463,     INVALID,
        };

        internal static readonly ushort[] ud_itab__14 = {
          /* 00 */         824,         952,     INVALID,     INVALID,
          /* 04 */     INVALID,     INVALID,     INVALID,     INVALID,
        };

        internal static readonly ushort[] ud_itab__15 = {
          /* 00 */        1485,        1508,     INVALID,     INVALID,
          /* 04 */     INVALID,     INVALID,     INVALID,     INVALID,
        };

        internal static readonly ushort[] ud_itab__16 = {
          /* 00 */   0x8000|17,   0x8000|18,   0x8000|19,   0x8000|20,
          /* 04 */   0x8000|21,   0x8000|22,   0x8000|23,   0x8000|24,
        };

        internal static readonly ushort[] ud_itab__17 = {
          /* 00 */        1466,     INVALID,     INVALID,
        };

        internal static readonly ushort[] ud_itab__18 = {
          /* 00 */        1467,     INVALID,     INVALID,
        };

        internal static readonly ushort[] ud_itab__19 = {
          /* 00 */        1468,     INVALID,     INVALID,
        };

        internal static readonly ushort[] ud_itab__20 = {
          /* 00 */        1469,     INVALID,     INVALID,
        };

        internal static readonly ushort[] ud_itab__21 = {
          /* 00 */        1397,     INVALID,     INVALID,
        };

        internal static readonly ushort[] ud_itab__22 = {
          /* 00 */          80,     INVALID,     INVALID,
        };

        internal static readonly ushort[] ud_itab__23 = {
          /* 00 */        1399,     INVALID,     INVALID,
        };

        internal static readonly ushort[] ud_itab__24 = {
          /* 00 */         720,     INVALID,     INVALID,
        };

        internal static readonly ushort[] ud_itab__25 = {
          /* 00 */        1425,   0x8000|26,     INVALID,     INVALID,
          /* 04 */     INVALID,     INVALID,     INVALID,     INVALID,
        };

        internal static readonly ushort[] ud_itab__26 = {
          /* 00 */        1298,     INVALID,     INVALID,
        };

        internal static readonly ushort[] ud_itab__27 = {
          /* 00 */        1119,        1120,        1121,        1122,
          /* 04 */        1123,        1124,        1125,        1126,
        };

        internal static readonly ushort[] ud_itab__28 = {
          /* 00 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 04 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 08 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 0c */        1216,        1217,     INVALID,     INVALID,
          /* 10 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 14 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 18 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 1c */        1218,        1219,     INVALID,     INVALID,
          /* 20 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 24 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 28 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 2c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 30 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 34 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 38 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 3c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 40 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 44 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 48 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 4c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 50 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 54 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 58 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 5c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 60 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 64 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 68 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 6c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 70 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 74 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 78 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 7c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 80 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 84 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 88 */     INVALID,     INVALID,        1220,     INVALID,
          /* 8c */     INVALID,     INVALID,        1221,     INVALID,
          /* 90 */        1222,     INVALID,     INVALID,     INVALID,
          /* 94 */        1223,     INVALID,        1224,        1225,
          /* 98 */     INVALID,     INVALID,        1226,     INVALID,
          /* 9c */     INVALID,     INVALID,        1227,     INVALID,
          /* a0 */        1228,     INVALID,     INVALID,     INVALID,
          /* a4 */        1229,     INVALID,        1230,        1231,
          /* a8 */     INVALID,     INVALID,        1232,     INVALID,
          /* ac */     INVALID,     INVALID,        1233,     INVALID,
          /* b0 */        1234,     INVALID,     INVALID,     INVALID,
          /* b4 */        1235,     INVALID,        1236,        1237,
          /* b8 */     INVALID,     INVALID,     INVALID,        1238,
          /* bc */     INVALID,     INVALID,     INVALID,        1239,
          /* c0 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* c4 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* c8 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* cc */     INVALID,     INVALID,     INVALID,     INVALID,
          /* d0 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* d4 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* d8 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* dc */     INVALID,     INVALID,     INVALID,     INVALID,
          /* e0 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* e4 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* e8 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* ec */     INVALID,     INVALID,     INVALID,     INVALID,
          /* f0 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* f4 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* f8 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* fc */     INVALID,     INVALID,     INVALID,     INVALID,
        };

        internal static readonly ushort[] ud_itab__29 = {
          /* 00 */         936,         925,         928,         932,
        };

        internal static readonly ushort[] ud_itab__30 = {
          /* 00 */         938,         926,         929,         934,
        };

        internal static readonly ushort[] ud_itab__31 = {
          /* 00 */   0x8000|32,   0x8000|33,
        };

        internal static readonly ushort[] ud_itab__32 = {
          /* 00 */         892,        1563,        1571,         888,
        };

        internal static readonly ushort[] ud_itab__33 = {
          /* 00 */         896,        1561,        1569,     INVALID,
        };

        internal static readonly ushort[] ud_itab__34 = {
          /* 00 */         894,     INVALID,     INVALID,         890,
        };

        internal static readonly ushort[] ud_itab__35 = {
          /* 00 */        1449,     INVALID,     INVALID,        1451,
        };

        internal static readonly ushort[] ud_itab__36 = {
          /* 00 */        1447,     INVALID,     INVALID,        1445,
        };

        internal static readonly ushort[] ud_itab__37 = {
          /* 00 */   0x8000|38,   0x8000|39,
        };

        internal static readonly ushort[] ud_itab__38 = {
          /* 00 */         882,     INVALID,        1567,         878,
        };

        internal static readonly ushort[] ud_itab__39 = {
          /* 00 */         886,     INVALID,        1565,     INVALID,
        };

        internal static readonly ushort[] ud_itab__40 = {
          /* 00 */         884,     INVALID,     INVALID,         880,
        };

        internal static readonly ushort[] ud_itab__41 = {
          /* 00 */        1127,        1128,        1129,        1130,
          /* 04 */     INVALID,     INVALID,     INVALID,     INVALID,
        };

        internal static readonly ushort[] ud_itab__42 = {
          /* 00 */         862,     INVALID,     INVALID,         858,
        };

        internal static readonly ushort[] ud_itab__43 = {
          /* 00 */         864,     INVALID,     INVALID,         860,
        };

        internal static readonly ushort[] ud_itab__44 = {
          /* 00 */         141,         152,         154,         142,
        };

        internal static readonly ushort[] ud_itab__45 = {
          /* 00 */         907,     INVALID,     INVALID,         905,
        };

        internal static readonly ushort[] ud_itab__46 = {
          /* 00 */         165,         166,         168,         162,
        };

        internal static readonly ushort[] ud_itab__47 = {
          /* 00 */         147,         148,         158,         138,
        };

        internal static readonly ushort[] ud_itab__48 = {
          /* 00 */        1442,     INVALID,     INVALID,        1440,
        };

        internal static readonly ushort[] ud_itab__49 = {
          /* 00 */         129,     INVALID,     INVALID,         127,
        };

        internal static readonly ushort[] ud_itab__50 = {
          /* 00 */        1427,   0x8000|51,
        };

        internal static readonly ushort[] ud_itab__51 = {
          /* 00 */     INVALID,        1428,     INVALID,
        };

        internal static readonly ushort[] ud_itab__52 = {
          /* 00 */        1429,   0x8000|53,
        };

        internal static readonly ushort[] ud_itab__53 = {
          /* 00 */     INVALID,        1430,     INVALID,
        };

        internal static readonly ushort[] ud_itab__54 = {
          /* 00 */   0x8000|55,   0x8000|56,   0x8000|57,   0x8000|58,
          /* 04 */   0x8000|59,   0x8000|60,   0x8000|61,   0x8000|62,
          /* 08 */   0x8000|63,   0x8000|64,   0x8000|65,   0x8000|66,
          /* 0c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 10 */   0x8000|67,     INVALID,     INVALID,     INVALID,
          /* 14 */   0x8000|68,   0x8000|69,     INVALID,   0x8000|70,
          /* 18 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 1c */   0x8000|71,   0x8000|72,   0x8000|73,     INVALID,
          /* 20 */   0x8000|74,   0x8000|75,   0x8000|76,   0x8000|77,
          /* 24 */   0x8000|78,   0x8000|79,     INVALID,     INVALID,
          /* 28 */   0x8000|80,   0x8000|81,   0x8000|82,   0x8000|83,
          /* 2c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 30 */   0x8000|84,   0x8000|85,   0x8000|86,   0x8000|87,
          /* 34 */   0x8000|88,   0x8000|89,     INVALID,   0x8000|90,
          /* 38 */   0x8000|91,   0x8000|92,   0x8000|93,   0x8000|94,
          /* 3c */   0x8000|95,   0x8000|96,   0x8000|97,   0x8000|98,
          /* 40 */   0x8000|99,  0x8000|100,     INVALID,     INVALID,
          /* 44 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 48 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 4c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 50 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 54 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 58 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 5c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 60 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 64 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 68 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 6c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 70 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 74 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 78 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 7c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 80 */  0x8000|101,  0x8000|105,     INVALID,     INVALID,
          /* 84 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 88 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 8c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 90 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 94 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 98 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 9c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* a0 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* a4 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* a8 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* ac */     INVALID,     INVALID,     INVALID,     INVALID,
          /* b0 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* b4 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* b8 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* bc */     INVALID,     INVALID,     INVALID,     INVALID,
          /* c0 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* c4 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* c8 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* cc */     INVALID,     INVALID,     INVALID,     INVALID,
          /* d0 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* d4 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* d8 */     INVALID,     INVALID,     INVALID,  0x8000|109,
          /* dc */  0x8000|110,  0x8000|111,  0x8000|112,  0x8000|113,
          /* e0 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* e4 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* e8 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* ec */     INVALID,     INVALID,     INVALID,     INVALID,
          /* f0 */  0x8000|114,  0x8000|115,     INVALID,     INVALID,
          /* f4 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* f8 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* fc */     INVALID,     INVALID,     INVALID,     INVALID,
        };

        internal static readonly ushort[] ud_itab__55 = {
          /* 00 */        1582,     INVALID,     INVALID,        1583,
        };

        internal static readonly ushort[] ud_itab__56 = {
          /* 00 */        1585,     INVALID,     INVALID,        1586,
        };

        internal static readonly ushort[] ud_itab__57 = {
          /* 00 */        1588,     INVALID,     INVALID,        1589,
        };

        internal static readonly ushort[] ud_itab__58 = {
          /* 00 */        1591,     INVALID,     INVALID,        1592,
        };

        internal static readonly ushort[] ud_itab__59 = {
          /* 00 */        1594,     INVALID,     INVALID,        1595,
        };

        internal static readonly ushort[] ud_itab__60 = {
          /* 00 */        1597,     INVALID,     INVALID,        1598,
        };

        internal static readonly ushort[] ud_itab__61 = {
          /* 00 */        1600,     INVALID,     INVALID,        1601,
        };

        internal static readonly ushort[] ud_itab__62 = {
          /* 00 */        1603,     INVALID,     INVALID,        1604,
        };

        internal static readonly ushort[] ud_itab__63 = {
          /* 00 */        1606,     INVALID,     INVALID,        1607,
        };

        internal static readonly ushort[] ud_itab__64 = {
          /* 00 */        1612,     INVALID,     INVALID,        1613,
        };

        internal static readonly ushort[] ud_itab__65 = {
          /* 00 */        1609,     INVALID,     INVALID,        1610,
        };

        internal static readonly ushort[] ud_itab__66 = {
          /* 00 */        1615,     INVALID,     INVALID,        1616,
        };

        internal static readonly ushort[] ud_itab__67 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1621,
        };

        internal static readonly ushort[] ud_itab__68 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1657,
        };

        internal static readonly ushort[] ud_itab__69 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1656,
        };

        internal static readonly ushort[] ud_itab__70 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1711,
        };

        internal static readonly ushort[] ud_itab__71 = {
          /* 00 */        1573,     INVALID,     INVALID,        1574,
        };

        internal static readonly ushort[] ud_itab__72 = {
          /* 00 */        1576,     INVALID,     INVALID,        1577,
        };

        internal static readonly ushort[] ud_itab__73 = {
          /* 00 */        1579,     INVALID,     INVALID,        1580,
        };

        internal static readonly ushort[] ud_itab__74 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1685,
        };

        internal static readonly ushort[] ud_itab__75 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1687,
        };

        internal static readonly ushort[] ud_itab__76 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1689,
        };

        internal static readonly ushort[] ud_itab__77 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1691,
        };

        internal static readonly ushort[] ud_itab__78 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1693,
        };

        internal static readonly ushort[] ud_itab__79 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1695,
        };

        internal static readonly ushort[] ud_itab__80 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1622,
        };

        internal static readonly ushort[] ud_itab__81 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1708,
        };

        internal static readonly ushort[] ud_itab__82 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1681,
        };

        internal static readonly ushort[] ud_itab__83 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1683,
        };

        internal static readonly ushort[] ud_itab__84 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1696,
        };

        internal static readonly ushort[] ud_itab__85 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1698,
        };

        internal static readonly ushort[] ud_itab__86 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1700,
        };

        internal static readonly ushort[] ud_itab__87 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1702,
        };

        internal static readonly ushort[] ud_itab__88 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1704,
        };

        internal static readonly ushort[] ud_itab__89 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1706,
        };

        internal static readonly ushort[] ud_itab__90 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1717,
        };

        internal static readonly ushort[] ud_itab__91 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1624,
        };

        internal static readonly ushort[] ud_itab__92 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1626,
        };

        internal static readonly ushort[] ud_itab__93 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1628,
        };

        internal static readonly ushort[] ud_itab__94 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1630,
        };

        internal static readonly ushort[] ud_itab__95 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1632,
        };

        internal static readonly ushort[] ud_itab__96 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1634,
        };

        internal static readonly ushort[] ud_itab__97 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1638,
        };

        internal static readonly ushort[] ud_itab__98 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1636,
        };

        internal static readonly ushort[] ud_itab__99 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1640,
        };

        internal static readonly ushort[] ud_itab__100 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1642,
        };

        internal static readonly ushort[] ud_itab__101 = {
          /* 00 */     INVALID,     INVALID,     INVALID,  0x8000|102,
        };

        internal static readonly ushort[] ud_itab__102 = {
          /* 00 */  0x8000|103,  0x8000|104,
        };

        internal static readonly ushort[] ud_itab__103 = {
          /* 00 */     INVALID,         717,     INVALID,
        };

        internal static readonly ushort[] ud_itab__104 = {
          /* 00 */     INVALID,         718,     INVALID,
        };

        internal static readonly ushort[] ud_itab__105 = {
          /* 00 */     INVALID,     INVALID,     INVALID,  0x8000|106,
        };

        internal static readonly ushort[] ud_itab__106 = {
          /* 00 */  0x8000|107,  0x8000|108,
        };

        internal static readonly ushort[] ud_itab__107 = {
          /* 00 */     INVALID,         721,     INVALID,
        };

        internal static readonly ushort[] ud_itab__108 = {
          /* 00 */     INVALID,         722,     INVALID,
        };

        internal static readonly ushort[] ud_itab__109 = {
          /* 00 */     INVALID,     INVALID,     INVALID,          45,
        };

        internal static readonly ushort[] ud_itab__110 = {
          /* 00 */     INVALID,     INVALID,     INVALID,          41,
        };

        internal static readonly ushort[] ud_itab__111 = {
          /* 00 */     INVALID,     INVALID,     INVALID,          43,
        };

        internal static readonly ushort[] ud_itab__112 = {
          /* 00 */     INVALID,     INVALID,     INVALID,          37,
        };

        internal static readonly ushort[] ud_itab__113 = {
          /* 00 */     INVALID,     INVALID,     INVALID,          39,
        };

        internal static readonly ushort[] ud_itab__114 = {
          /* 00 */        1723,        1725,     INVALID,     INVALID,
        };

        internal static readonly ushort[] ud_itab__115 = {
          /* 00 */        1724,        1726,     INVALID,     INVALID,
        };

        internal static readonly ushort[] ud_itab__116 = {
          /* 00 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 04 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 08 */  0x8000|117,  0x8000|118,  0x8000|119,  0x8000|120,
          /* 0c */  0x8000|121,  0x8000|122,  0x8000|123,  0x8000|124,
          /* 10 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 14 */  0x8000|125,  0x8000|126,  0x8000|127,  0x8000|129,
          /* 18 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 1c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 20 */  0x8000|130,  0x8000|131,  0x8000|132,     INVALID,
          /* 24 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 28 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 2c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 30 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 34 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 38 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 3c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 40 */  0x8000|134,  0x8000|135,  0x8000|136,     INVALID,
          /* 44 */  0x8000|137,     INVALID,     INVALID,     INVALID,
          /* 48 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 4c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 50 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 54 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 58 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 5c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 60 */  0x8000|138,  0x8000|139,  0x8000|140,  0x8000|141,
          /* 64 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 68 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 6c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 70 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 74 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 78 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 7c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 80 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 84 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 88 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 8c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 90 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 94 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 98 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 9c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* a0 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* a4 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* a8 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* ac */     INVALID,     INVALID,     INVALID,     INVALID,
          /* b0 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* b4 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* b8 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* bc */     INVALID,     INVALID,     INVALID,     INVALID,
          /* c0 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* c4 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* c8 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* cc */     INVALID,     INVALID,     INVALID,     INVALID,
          /* d0 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* d4 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* d8 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* dc */     INVALID,     INVALID,     INVALID,  0x8000|142,
          /* e0 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* e4 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* e8 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* ec */     INVALID,     INVALID,     INVALID,     INVALID,
          /* f0 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* f4 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* f8 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* fc */     INVALID,     INVALID,     INVALID,     INVALID,
        };

        internal static readonly ushort[] ud_itab__117 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1644,
        };

        internal static readonly ushort[] ud_itab__118 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1646,
        };

        internal static readonly ushort[] ud_itab__119 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1648,
        };

        internal static readonly ushort[] ud_itab__120 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1650,
        };

        internal static readonly ushort[] ud_itab__121 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1654,
        };

        internal static readonly ushort[] ud_itab__122 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1652,
        };

        internal static readonly ushort[] ud_itab__123 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1677,
        };

        internal static readonly ushort[] ud_itab__124 = {
          /* 00 */        1618,     INVALID,     INVALID,        1619,
        };

        internal static readonly ushort[] ud_itab__125 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1045,
        };

        internal static readonly ushort[] ud_itab__126 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1056,
        };

        internal static readonly ushort[] ud_itab__127 = {
          /* 00 */     INVALID,     INVALID,     INVALID,  0x8000|128,
        };

        internal static readonly ushort[] ud_itab__128 = {
          /* 00 */        1047,        1049,        1051,
        };

        internal static readonly ushort[] ud_itab__129 = {
          /* 00 */     INVALID,     INVALID,     INVALID,         201,
        };

        internal static readonly ushort[] ud_itab__130 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1058,
        };

        internal static readonly ushort[] ud_itab__131 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1557,
        };

        internal static readonly ushort[] ud_itab__132 = {
          /* 00 */     INVALID,     INVALID,     INVALID,  0x8000|133,
        };

        internal static readonly ushort[] ud_itab__133 = {
          /* 00 */        1062,        1063,        1064,
        };

        internal static readonly ushort[] ud_itab__134 = {
          /* 00 */     INVALID,     INVALID,     INVALID,         197,
        };

        internal static readonly ushort[] ud_itab__135 = {
          /* 00 */     INVALID,     INVALID,     INVALID,         195,
        };

        internal static readonly ushort[] ud_itab__136 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1679,
        };

        internal static readonly ushort[] ud_itab__137 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1512,
        };

        internal static readonly ushort[] ud_itab__138 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1715,
        };

        internal static readonly ushort[] ud_itab__139 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1713,
        };

        internal static readonly ushort[] ud_itab__140 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1721,
        };

        internal static readonly ushort[] ud_itab__141 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1719,
        };

        internal static readonly ushort[] ud_itab__142 = {
          /* 00 */     INVALID,     INVALID,     INVALID,          47,
        };

        internal static readonly ushort[] ud_itab__143 = {
          /* 00 */         900,     INVALID,     INVALID,         898,
        };

        internal static readonly ushort[] ud_itab__144 = {
          /* 00 */        1387,        1391,        1393,        1389,
        };

        internal static readonly ushort[] ud_itab__145 = {
          /* 00 */        1306,     INVALID,        1308,     INVALID,
        };

        internal static readonly ushort[] ud_itab__146 = {
          /* 00 */        1291,     INVALID,        1293,     INVALID,
        };

        internal static readonly ushort[] ud_itab__147 = {
          /* 00 */          61,     INVALID,     INVALID,          59,
        };

        internal static readonly ushort[] ud_itab__148 = {
          /* 00 */          65,     INVALID,     INVALID,          63,
        };

        internal static readonly ushort[] ud_itab__149 = {
          /* 00 */         976,     INVALID,     INVALID,         974,
        };

        internal static readonly ushort[] ud_itab__150 = {
          /* 00 */        1499,     INVALID,     INVALID,        1497,
        };

        internal static readonly ushort[] ud_itab__151 = {
          /* 00 */          27,          29,          31,          25,
        };

        internal static readonly ushort[] ud_itab__152 = {
          /* 00 */         946,         948,         950,         944,
        };

        internal static readonly ushort[] ud_itab__153 = {
          /* 00 */         145,         150,         156,         139,
        };

        internal static readonly ushort[] ud_itab__154 = {
          /* 00 */         134,     INVALID,         163,         143,
        };

        internal static readonly ushort[] ud_itab__155 = {
          /* 00 */        1419,        1421,        1423,        1417,
        };

        internal static readonly ushort[] ud_itab__156 = {
          /* 00 */         818,         820,         822,         816,
        };

        internal static readonly ushort[] ud_itab__157 = {
          /* 00 */         189,         191,         193,         187,
        };

        internal static readonly ushort[] ud_itab__158 = {
          /* 00 */         802,         804,         806,         800,
        };

        internal static readonly ushort[] ud_itab__159 = {
          /* 00 */        1209,     INVALID,     INVALID,        1207,
        };

        internal static readonly ushort[] ud_itab__160 = {
          /* 00 */        1212,     INVALID,     INVALID,        1210,
        };

        internal static readonly ushort[] ud_itab__161 = {
          /* 00 */        1215,     INVALID,     INVALID,        1213,
        };

        internal static readonly ushort[] ud_itab__162 = {
          /* 00 */         987,     INVALID,     INVALID,         985,
        };

        internal static readonly ushort[] ud_itab__163 = {
          /* 00 */        1038,     INVALID,     INVALID,        1036,
        };

        internal static readonly ushort[] ud_itab__164 = {
          /* 00 */        1041,     INVALID,     INVALID,        1039,
        };

        internal static readonly ushort[] ud_itab__165 = {
          /* 00 */        1044,     INVALID,     INVALID,        1042,
        };

        internal static readonly ushort[] ud_itab__166 = {
          /* 00 */         993,     INVALID,     INVALID,         991,
        };

        internal static readonly ushort[] ud_itab__167 = {
          /* 00 */        1200,     INVALID,     INVALID,        1198,
        };

        internal static readonly ushort[] ud_itab__168 = {
          /* 00 */        1203,     INVALID,     INVALID,        1201,
        };

        internal static readonly ushort[] ud_itab__169 = {
          /* 00 */        1206,     INVALID,     INVALID,        1204,
        };

        internal static readonly ushort[] ud_itab__170 = {
          /* 00 */         990,     INVALID,     INVALID,         988,
        };

        internal static readonly ushort[] ud_itab__171 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1547,
        };

        internal static readonly ushort[] ud_itab__172 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1545,
        };

        internal static readonly ushort[] ud_itab__173 = {
          /* 00 */  0x8000|174,     INVALID,     INVALID,  0x8000|175,
        };

        internal static readonly ushort[] ud_itab__174 = {
          /* 00 */         866,         867,         910,
        };

        internal static readonly ushort[] ud_itab__175 = {
          /* 00 */         868,         870,         911,
        };

        internal static readonly ushort[] ud_itab__176 = {
          /* 00 */         920,     INVALID,        1522,        1517,
        };

        internal static readonly ushort[] ud_itab__177 = {
          /* 00 */        1134,        1537,        1535,        1539,
        };

        internal static readonly ushort[] ud_itab__178 = {
          /* 00 */     INVALID,     INVALID,  0x8000|179,     INVALID,
          /* 04 */  0x8000|180,     INVALID,  0x8000|181,     INVALID,
        };

        internal static readonly ushort[] ud_itab__179 = {
          /* 00 */        1159,     INVALID,     INVALID,        1163,
        };

        internal static readonly ushort[] ud_itab__180 = {
          /* 00 */        1152,     INVALID,     INVALID,        1150,
        };

        internal static readonly ushort[] ud_itab__181 = {
          /* 00 */        1138,     INVALID,     INVALID,        1137,
        };

        internal static readonly ushort[] ud_itab__182 = {
          /* 00 */     INVALID,     INVALID,  0x8000|183,     INVALID,
          /* 04 */  0x8000|184,     INVALID,  0x8000|185,     INVALID,
        };

        internal static readonly ushort[] ud_itab__183 = {
          /* 00 */        1165,     INVALID,     INVALID,        1169,
        };

        internal static readonly ushort[] ud_itab__184 = {
          /* 00 */        1153,     INVALID,     INVALID,        1157,
        };

        internal static readonly ushort[] ud_itab__185 = {
          /* 00 */        1142,     INVALID,     INVALID,        1141,
        };

        internal static readonly ushort[] ud_itab__186 = {
          /* 00 */     INVALID,     INVALID,  0x8000|187,  0x8000|188,
          /* 04 */     INVALID,     INVALID,  0x8000|189,  0x8000|190,
        };

        internal static readonly ushort[] ud_itab__187 = {
          /* 00 */        1171,     INVALID,     INVALID,        1175,
        };

        internal static readonly ushort[] ud_itab__188 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1543,
        };

        internal static readonly ushort[] ud_itab__189 = {
          /* 00 */        1146,     INVALID,     INVALID,        1145,
        };

        internal static readonly ushort[] ud_itab__190 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1541,
        };

        internal static readonly ushort[] ud_itab__191 = {
          /* 00 */        1027,     INVALID,     INVALID,        1028,
        };

        internal static readonly ushort[] ud_itab__192 = {
          /* 00 */        1030,     INVALID,     INVALID,        1031,
        };

        internal static readonly ushort[] ud_itab__193 = {
          /* 00 */        1033,     INVALID,     INVALID,        1034,
        };

        internal static readonly ushort[] ud_itab__194 = {
          /* 00 */     INVALID,        1464,     INVALID,
        };

        internal static readonly ushort[] ud_itab__195 = {
          /* 00 */     INVALID,        1465,     INVALID,
        };

        internal static readonly ushort[] ud_itab__196 = {
          /* 00 */     INVALID,        1551,     INVALID,        1549,
        };

        internal static readonly ushort[] ud_itab__197 = {
          /* 00 */     INVALID,        1555,     INVALID,        1553,
        };

        internal static readonly ushort[] ud_itab__198 = {
          /* 00 */  0x8000|199,     INVALID,         916,  0x8000|200,
        };

        internal static readonly ushort[] ud_itab__199 = {
          /* 00 */         872,         873,         913,
        };

        internal static readonly ushort[] ud_itab__200 = {
          /* 00 */         874,         876,         914,
        };

        internal static readonly ushort[] ud_itab__201 = {
          /* 00 */         921,     INVALID,        1524,        1515,
        };

        internal static readonly ushort[] ud_itab__202 = {
          /* 00 */     INVALID,  0x8000|203,
        };

        internal static readonly ushort[] ud_itab__203 = {
          /* 00 */  0x8000|204,  0x8000|205,  0x8000|206,     INVALID,
          /* 04 */     INVALID,     INVALID,     INVALID,     INVALID,
        };

        internal static readonly ushort[] ud_itab__204 = {
          /* 00 */         825,     INVALID,     INVALID,     INVALID,
          /* 04 */     INVALID,     INVALID,     INVALID,     INVALID,
        };

        internal static readonly ushort[] ud_itab__205 = {
          /* 00 */        1509,     INVALID,     INVALID,     INVALID,
          /* 04 */     INVALID,     INVALID,     INVALID,     INVALID,
        };

        internal static readonly ushort[] ud_itab__206 = {
          /* 00 */        1510,     INVALID,     INVALID,     INVALID,
          /* 04 */     INVALID,     INVALID,     INVALID,     INVALID,
        };

        internal static readonly ushort[] ud_itab__207 = {
          /* 00 */     INVALID,  0x8000|208,
        };

        internal static readonly ushort[] ud_itab__208 = {
          /* 00 */  0x8000|209,  0x8000|210,  0x8000|211,  0x8000|212,
          /* 04 */  0x8000|213,  0x8000|214,     INVALID,     INVALID,
        };

        internal static readonly ushort[] ud_itab__209 = {
          /* 00 */        1511,     INVALID,     INVALID,     INVALID,
          /* 04 */     INVALID,     INVALID,     INVALID,     INVALID,
        };

        internal static readonly ushort[] ud_itab__210 = {
          /* 00 */        1501,     INVALID,     INVALID,     INVALID,
          /* 04 */     INVALID,     INVALID,     INVALID,     INVALID,
        };

        internal static readonly ushort[] ud_itab__211 = {
          /* 00 */        1502,     INVALID,     INVALID,     INVALID,
          /* 04 */     INVALID,     INVALID,     INVALID,     INVALID,
        };

        internal static readonly ushort[] ud_itab__212 = {
          /* 00 */        1503,     INVALID,     INVALID,     INVALID,
          /* 04 */     INVALID,     INVALID,     INVALID,     INVALID,
        };

        internal static readonly ushort[] ud_itab__213 = {
          /* 00 */        1504,     INVALID,     INVALID,     INVALID,
          /* 04 */     INVALID,     INVALID,     INVALID,     INVALID,
        };

        internal static readonly ushort[] ud_itab__214 = {
          /* 00 */        1505,     INVALID,     INVALID,     INVALID,
          /* 04 */     INVALID,     INVALID,     INVALID,     INVALID,
        };

        internal static readonly ushort[] ud_itab__215 = {
          /* 00 */  0x8000|216,  0x8000|217,
        };

        internal static readonly ushort[] ud_itab__216 = {
          /* 00 */         683,         682,         768,        1400,
          /* 04 */        1507,        1506,     INVALID,          79,
        };

        internal static readonly ushort[] ud_itab__217 = {
          /* 00 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 04 */     INVALID,  0x8000|218,  0x8000|219,  0x8000|220,
        };

        internal static readonly ushort[] ud_itab__218 = {
          /* 00 */         777,         778,         779,         780,
          /* 04 */         781,         782,         783,         784,
        };

        internal static readonly ushort[] ud_itab__219 = {
          /* 00 */         808,         809,         810,         811,
          /* 04 */         812,         813,         814,         815,
        };

        internal static readonly ushort[] ud_itab__220 = {
          /* 00 */        1366,        1367,        1368,        1369,
          /* 04 */        1370,        1371,        1372,        1373,
        };

        internal static readonly ushort[] ud_itab__221 = {
          /* 00 */     INVALID,     INVALID,        1710,     INVALID,
        };

        internal static readonly ushort[] ud_itab__222 = {
          /* 00 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 04 */        1669,        1676,        1674,        1672,
        };

        internal static readonly ushort[] ud_itab__223 = {
          /* 00 */         112,         117,         120,         110,
        };

        internal static readonly ushort[] ud_itab__224 = {
          /* 00 */        1059,     INVALID,     INVALID,        1060,
        };

        internal static readonly ushort[] ud_itab__225 = {
          /* 00 */        1055,     INVALID,     INVALID,        1053,
        };

        internal static readonly ushort[] ud_itab__226 = {
          /* 00 */        1381,     INVALID,     INVALID,        1379,
        };

        internal static readonly ushort[] ud_itab__227 = {
          /* 00 */  0x8000|228,  0x8000|235,
        };

        internal static readonly ushort[] ud_itab__228 = {
          /* 00 */     INVALID,  0x8000|229,     INVALID,     INVALID,
          /* 04 */     INVALID,     INVALID,  0x8000|230,  0x8000|234,
        };

        internal static readonly ushort[] ud_itab__229 = {
          /* 00 */         124,         125,         126,
        };

        internal static readonly ushort[] ud_itab__230 = {
          /* 00 */  0x8000|231,     INVALID,  0x8000|232,  0x8000|233,
        };

        internal static readonly ushort[] ud_itab__231 = {
          /* 00 */     INVALID,        1459,     INVALID,
        };

        internal static readonly ushort[] ud_itab__232 = {
          /* 00 */     INVALID,        1458,     INVALID,
        };

        internal static readonly ushort[] ud_itab__233 = {
          /* 00 */     INVALID,        1457,     INVALID,
        };

        internal static readonly ushort[] ud_itab__234 = {
          /* 00 */     INVALID,        1460,     INVALID,
        };

        internal static readonly ushort[] ud_itab__235 = {
          /* 00 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 04 */     INVALID,     INVALID,        1456,     INVALID,
        };

        internal static readonly ushort[] ud_itab__236 = {
          /* 00 */     INVALID,          35,     INVALID,          33,
        };

        internal static readonly ushort[] ud_itab__237 = {
          /* 00 */        1160,     INVALID,     INVALID,        1161,
        };

        internal static readonly ushort[] ud_itab__238 = {
          /* 00 */        1166,     INVALID,     INVALID,        1167,
        };

        internal static readonly ushort[] ud_itab__239 = {
          /* 00 */        1172,     INVALID,     INVALID,        1173,
        };

        internal static readonly ushort[] ud_itab__240 = {
          /* 00 */        1527,     INVALID,     INVALID,        1528,
        };

        internal static readonly ushort[] ud_itab__241 = {
          /* 00 */        1093,     INVALID,     INVALID,        1094,
        };

        internal static readonly ushort[] ud_itab__242 = {
          /* 00 */     INVALID,        1521,        1526,         918,
        };

        internal static readonly ushort[] ud_itab__243 = {
          /* 00 */        1086,     INVALID,     INVALID,        1084,
        };

        internal static readonly ushort[] ud_itab__244 = {
          /* 00 */        1192,     INVALID,     INVALID,        1193,
        };

        internal static readonly ushort[] ud_itab__245 = {
          /* 00 */        1195,     INVALID,     INVALID,        1196,
        };

        internal static readonly ushort[] ud_itab__246 = {
          /* 00 */        1083,     INVALID,     INVALID,        1081,
        };

        internal static readonly ushort[] ud_itab__247 = {
          /* 00 */        1017,     INVALID,     INVALID,        1015,
        };

        internal static readonly ushort[] ud_itab__248 = {
          /* 00 */        1009,     INVALID,     INVALID,        1010,
        };

        internal static readonly ushort[] ud_itab__249 = {
          /* 00 */        1012,     INVALID,     INVALID,        1013,
        };

        internal static readonly ushort[] ud_itab__250 = {
          /* 00 */        1075,     INVALID,     INVALID,        1076,
        };

        internal static readonly ushort[] ud_itab__251 = {
          /* 00 */        1020,     INVALID,     INVALID,        1018,
        };

        internal static readonly ushort[] ud_itab__252 = {
          /* 00 */        1023,     INVALID,     INVALID,        1021,
        };

        internal static readonly ushort[] ud_itab__253 = {
          /* 00 */        1147,     INVALID,     INVALID,        1148,
        };

        internal static readonly ushort[] ud_itab__254 = {
          /* 00 */        1156,     INVALID,     INVALID,        1154,
        };

        internal static readonly ushort[] ud_itab__255 = {
          /* 00 */        1026,     INVALID,     INVALID,        1024,
        };

        internal static readonly ushort[] ud_itab__256 = {
          /* 00 */        1087,     INVALID,     INVALID,        1088,
        };

        internal static readonly ushort[] ud_itab__257 = {
          /* 00 */        1092,     INVALID,     INVALID,        1090,
        };

        internal static readonly ushort[] ud_itab__258 = {
          /* 00 */     INVALID,         136,         132,         160,
        };

        internal static readonly ushort[] ud_itab__259 = {
          /* 00 */         909,     INVALID,     INVALID,         902,
        };

        internal static readonly ushort[] ud_itab__260 = {
          /* 00 */        1186,     INVALID,     INVALID,        1187,
        };

        internal static readonly ushort[] ud_itab__261 = {
          /* 00 */        1189,     INVALID,     INVALID,        1190,
        };

        internal static readonly ushort[] ud_itab__262 = {
          /* 00 */        1080,     INVALID,     INVALID,        1078,
        };

        internal static readonly ushort[] ud_itab__263 = {
          /* 00 */        1118,     INVALID,     INVALID,        1116,
        };

        internal static readonly ushort[] ud_itab__264 = {
          /* 00 */        1003,     INVALID,     INVALID,        1004,
        };

        internal static readonly ushort[] ud_itab__265 = {
          /* 00 */        1006,     INVALID,     INVALID,        1007,
        };

        internal static readonly ushort[] ud_itab__266 = {
          /* 00 */        1074,     INVALID,     INVALID,        1072,
        };

        internal static readonly ushort[] ud_itab__267 = {
          /* 00 */        1266,     INVALID,     INVALID,        1264,
        };

        internal static readonly ushort[] ud_itab__268 = {
          /* 00 */     INVALID,        1559,     INVALID,     INVALID,
        };

        internal static readonly ushort[] ud_itab__269 = {
          /* 00 */        1136,     INVALID,     INVALID,        1135,
        };

        internal static readonly ushort[] ud_itab__270 = {
          /* 00 */        1140,     INVALID,     INVALID,        1139,
        };

        internal static readonly ushort[] ud_itab__271 = {
          /* 00 */        1144,     INVALID,     INVALID,        1143,
        };

        internal static readonly ushort[] ud_itab__272 = {
          /* 00 */        1533,     INVALID,     INVALID,        1534,
        };

        internal static readonly ushort[] ud_itab__273 = {
          /* 00 */        1069,     INVALID,     INVALID,        1070,
        };

        internal static readonly ushort[] ud_itab__274 = {
          /* 00 */        1133,     INVALID,     INVALID,        1131,
        };

        internal static readonly ushort[] ud_itab__275 = {
          /* 00 */     INVALID,  0x8000|276,
        };

        internal static readonly ushort[] ud_itab__276 = {
          /* 00 */         799,     INVALID,     INVALID,        1519,
        };

        internal static readonly ushort[] ud_itab__277 = {
          /* 00 */        1179,     INVALID,     INVALID,        1177,
        };

        internal static readonly ushort[] ud_itab__278 = {
          /* 00 */        1182,     INVALID,     INVALID,        1180,
        };

        internal static readonly ushort[] ud_itab__279 = {
          /* 00 */        1183,     INVALID,     INVALID,        1184,
        };

        internal static readonly ushort[] ud_itab__280 = {
          /* 00 */        1532,     INVALID,     INVALID,        1530,
        };

        internal static readonly ushort[] ud_itab__281 = {
          /* 00 */         996,     INVALID,     INVALID,         994,
        };

        internal static readonly ushort[] ud_itab__282 = {
          /* 00 */         997,     INVALID,     INVALID,         998,
        };

        internal static readonly ushort[] ud_itab__283 = {
          /* 00 */        1000,     INVALID,     INVALID,        1001,
        };

        internal static readonly ushort[] ud_itab__284 = {
          /* 00 */        1242,     INVALID,
        };

        internal static readonly ushort[] ud_itab__285 = {
          /* 00 */        1097,     INVALID,
        };

        internal static readonly ushort[] ud_itab__286 = {
          /* 00 */        1243,     INVALID,
        };

        internal static readonly ushort[] ud_itab__287 = {
          /* 00 */        1098,     INVALID,
        };

        internal static readonly ushort[] ud_itab__288 = {
          /* 00 */         173,     INVALID,
        };

        internal static readonly ushort[] ud_itab__289 = {
          /* 00 */         174,     INVALID,
        };

        internal static readonly ushort[] ud_itab__290 = {
          /* 00 */           1,     INVALID,
        };

        internal static readonly ushort[] ud_itab__291 = {
          /* 00 */           4,     INVALID,
        };

        internal static readonly ushort[] ud_itab__292 = {
          /* 00 */  0x8000|293,  0x8000|294,     INVALID,
        };

        internal static readonly ushort[] ud_itab__293 = {
          /* 00 */        1257,     INVALID,
        };

        internal static readonly ushort[] ud_itab__294 = {
          /* 00 */        1258,     INVALID,
        };

        internal static readonly ushort[] ud_itab__295 = {
          /* 00 */  0x8000|296,  0x8000|297,     INVALID,
        };

        internal static readonly ushort[] ud_itab__296 = {
          /* 00 */        1110,     INVALID,
        };

        internal static readonly ushort[] ud_itab__297 = {
          /* 00 */        1111,     INVALID,
        };

        internal static readonly ushort[] ud_itab__298 = {
          /* 00 */        1658,     INVALID,
        };

        internal static readonly ushort[] ud_itab__299 = {
          /* 00 */          67,          68,
        };

        internal static readonly ushort[] ud_itab__300 = {
          /* 00 */         710,         711,     INVALID,
        };

        internal static readonly ushort[] ud_itab__301 = {
          /* 00 */         983,         984,     INVALID,
        };

        internal static readonly ushort[] ud_itab__302 = {
          /* 00 */          21,         970,          11,        1342,
          /* 04 */          55,        1413,        1493,         106,
        };

        internal static readonly ushort[] ud_itab__303 = {
          /* 00 */          23,         971,          13,        1343,
          /* 04 */          57,        1414,        1494,         108,
        };

        internal static readonly ushort[] ud_itab__304 = {
          /* 00 */  0x8000|305,  0x8000|306,  0x8000|307,  0x8000|308,
          /* 04 */  0x8000|309,  0x8000|310,  0x8000|311,  0x8000|312,
        };

        internal static readonly ushort[] ud_itab__305 = {
          /* 00 */          22,     INVALID,
        };

        internal static readonly ushort[] ud_itab__306 = {
          /* 00 */         972,     INVALID,
        };

        internal static readonly ushort[] ud_itab__307 = {
          /* 00 */          12,     INVALID,
        };

        internal static readonly ushort[] ud_itab__308 = {
          /* 00 */        1344,     INVALID,
        };

        internal static readonly ushort[] ud_itab__309 = {
          /* 00 */          56,     INVALID,
        };

        internal static readonly ushort[] ud_itab__310 = {
          /* 00 */        1415,     INVALID,
        };

        internal static readonly ushort[] ud_itab__311 = {
          /* 00 */        1495,     INVALID,
        };

        internal static readonly ushort[] ud_itab__312 = {
          /* 00 */         107,     INVALID,
        };

        internal static readonly ushort[] ud_itab__313 = {
          /* 00 */          24,         973,          14,        1345,
          /* 04 */          58,        1416,        1496,         109,
        };

        internal static readonly ushort[] ud_itab__314 = {
          /* 00 */        1109,     INVALID,     INVALID,     INVALID,
          /* 04 */     INVALID,     INVALID,     INVALID,     INVALID,
        };

        internal static readonly ushort[] ud_itab__315 = {
          /* 00 */          74,          75,          76,
        };

        internal static readonly ushort[] ud_itab__316 = {
          /* 00 */         170,         171,         172,
        };

        internal static readonly ushort[] ud_itab__317 = {
          /* 00 */          73,     INVALID,
        };

        internal static readonly ushort[] ud_itab__318 = {
          /* 00 */  0x8000|319,  0x8000|320,  0x8000|321,
        };

        internal static readonly ushort[] ud_itab__319 = {
          /* 00 */        1259,        1260,
        };

        internal static readonly ushort[] ud_itab__320 = {
          /* 00 */        1261,        1262,
        };

        internal static readonly ushort[] ud_itab__321 = {
          /* 00 */     INVALID,        1263,
        };

        internal static readonly ushort[] ud_itab__322 = {
          /* 00 */  0x8000|323,  0x8000|324,  0x8000|325,
        };

        internal static readonly ushort[] ud_itab__323 = {
          /* 00 */        1112,     INVALID,
        };

        internal static readonly ushort[] ud_itab__324 = {
          /* 00 */        1113,        1114,
        };

        internal static readonly ushort[] ud_itab__325 = {
          /* 00 */     INVALID,        1115,
        };

        internal static readonly ushort[] ud_itab__326 = {
          /* 00 */         923,         924,         927,
        };

        internal static readonly ushort[] ud_itab__327 = {
          /* 00 */         115,         116,         119,
        };

        internal static readonly ushort[] ud_itab__328 = {
          /* 00 */        1403,        1404,        1405,
        };

        internal static readonly ushort[] ud_itab__329 = {
          /* 00 */         791,         792,         793,
        };

        internal static readonly ushort[] ud_itab__330 = {
          /* 00 */        1347,        1348,        1349,
        };

        internal static readonly ushort[] ud_itab__331 = {
          /* 00 */        1279,        1286,        1267,        1275,
          /* 04 */        1327,        1334,        1318,        1313,
        };

        internal static readonly ushort[] ud_itab__332 = {
          /* 00 */        1284,        1287,        1268,        1274,
          /* 04 */        1323,        1330,        1319,        1315,
        };

        internal static readonly ushort[] ud_itab__333 = {
          /* 00 */  0x8000|334,  0x8000|335,     INVALID,     INVALID,
          /* 04 */     INVALID,  0x8000|341,  0x8000|357,  0x8000|369,
          /* 08 */     INVALID,  0x8000|394,     INVALID,     INVALID,
          /* 0c */     INVALID,  0x8000|399,     INVALID,     INVALID,
        };

        internal static readonly ushort[] ud_itab__334 = {
          /* 00 */         771,     INVALID,
        };

        internal static readonly ushort[] ud_itab__335 = {
          /* 00 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 04 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 08 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 0c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 10 */         937,         939,  0x8000|336,         895,
          /* 14 */        1450,        1448,  0x8000|337,         885,
          /* 18 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 1c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 20 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 24 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 28 */         863,         865,     INVALID,         908,
          /* 2c */     INVALID,     INVALID,        1443,         130,
          /* 30 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 34 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 38 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 3c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 40 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 44 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 48 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 4c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 50 */         901,        1388,        1307,        1292,
          /* 54 */          62,          66,         977,        1500,
          /* 58 */          28,         947,         146,         135,
          /* 5c */        1420,         819,         190,         803,
          /* 60 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 64 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 68 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 6c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 70 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 74 */     INVALID,     INVALID,     INVALID,  0x8000|338,
          /* 78 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 7c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 80 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 84 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 88 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 8c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 90 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 94 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 98 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 9c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* a0 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* a4 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* a8 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* ac */     INVALID,     INVALID,  0x8000|339,     INVALID,
          /* b0 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* b4 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* b8 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* bc */     INVALID,     INVALID,     INVALID,     INVALID,
          /* c0 */     INVALID,     INVALID,         113,     INVALID,
          /* c4 */     INVALID,     INVALID,        1382,     INVALID,
          /* c8 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* cc */     INVALID,     INVALID,     INVALID,     INVALID,
          /* d0 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* d4 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* d8 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* dc */     INVALID,     INVALID,     INVALID,     INVALID,
          /* e0 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* e4 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* e8 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* ec */     INVALID,     INVALID,     INVALID,     INVALID,
          /* f0 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* f4 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* f8 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* fc */     INVALID,     INVALID,     INVALID,     INVALID,
        };

        internal static readonly ushort[] ud_itab__336 = {
          /* 00 */         893,         897,
        };

        internal static readonly ushort[] ud_itab__337 = {
          /* 00 */         883,         887,
        };

        internal static readonly ushort[] ud_itab__338 = {
          /* 00 */        1742,        1743,
        };

        internal static readonly ushort[] ud_itab__339 = {
          /* 00 */  0x8000|340,     INVALID,
        };

        internal static readonly ushort[] ud_itab__340 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1401,
          /* 04 */     INVALID,     INVALID,     INVALID,     INVALID,
        };

        internal static readonly ushort[] ud_itab__341 = {
          /* 00 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 04 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 08 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 0c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 10 */         933,         935,  0x8000|342,         891,
          /* 14 */        1452,        1446,  0x8000|343,         881,
          /* 18 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 1c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 20 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 24 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 28 */         859,         861,     INVALID,         906,
          /* 2c */     INVALID,     INVALID,        1441,         128,
          /* 30 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 34 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 38 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 3c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 40 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 44 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 48 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 4c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 50 */         899,        1390,     INVALID,     INVALID,
          /* 54 */          60,          64,         975,        1498,
          /* 58 */          26,         945,         140,         144,
          /* 5c */        1418,         817,         188,         801,
          /* 60 */        1208,        1211,        1214,         986,
          /* 64 */        1037,        1040,        1043,         992,
          /* 68 */        1199,        1202,        1205,         989,
          /* 6c */        1548,        1546,  0x8000|344,        1518,
          /* 70 */        1540,  0x8000|345,  0x8000|347,  0x8000|349,
          /* 74 */        1029,        1032,        1035,     INVALID,
          /* 78 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 7c */        1550,        1554,  0x8000|351,        1516,
          /* 80 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 84 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 88 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 8c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 90 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 94 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 98 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 9c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* a0 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* a4 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* a8 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* ac */     INVALID,     INVALID,     INVALID,     INVALID,
          /* b0 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* b4 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* b8 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* bc */     INVALID,     INVALID,     INVALID,     INVALID,
          /* c0 */     INVALID,     INVALID,         111,     INVALID,
          /* c4 */        1061,        1054,        1380,     INVALID,
          /* c8 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* cc */     INVALID,     INVALID,     INVALID,     INVALID,
          /* d0 */          34,        1162,        1168,        1174,
          /* d4 */        1529,        1095,         919,  0x8000|352,
          /* d8 */        1194,        1197,        1082,        1016,
          /* dc */        1011,        1014,        1077,        1019,
          /* e0 */        1022,        1149,        1155,        1025,
          /* e4 */        1089,        1091,         161,         903,
          /* e8 */        1188,        1191,        1079,        1117,
          /* ec */        1005,        1008,        1073,        1265,
          /* f0 */     INVALID,  0x8000|353,  0x8000|354,  0x8000|355,
          /* f4 */     INVALID,        1071,        1132,  0x8000|356,
          /* f8 */        1178,        1181,        1185,        1531,
          /* fc */         995,         999,        1002,     INVALID,
        };

        internal static readonly ushort[] ud_itab__342 = {
          /* 00 */         889,     INVALID,
        };

        internal static readonly ushort[] ud_itab__343 = {
          /* 00 */         879,     INVALID,
        };

        internal static readonly ushort[] ud_itab__344 = {
          /* 00 */         869,         871,         912,
        };

        internal static readonly ushort[] ud_itab__345 = {
          /* 00 */     INVALID,     INVALID,        1164,     INVALID,
          /* 04 */        1151,     INVALID,  0x8000|346,     INVALID,
        };

        internal static readonly ushort[] ud_itab__346 = {
          /* 00 */        1756,     INVALID,
        };

        internal static readonly ushort[] ud_itab__347 = {
          /* 00 */     INVALID,     INVALID,        1170,     INVALID,
          /* 04 */        1158,     INVALID,  0x8000|348,     INVALID,
        };

        internal static readonly ushort[] ud_itab__348 = {
          /* 00 */        1758,     INVALID,
        };

        internal static readonly ushort[] ud_itab__349 = {
          /* 00 */     INVALID,     INVALID,        1176,        1544,
          /* 04 */     INVALID,     INVALID,  0x8000|350,        1542,
        };

        internal static readonly ushort[] ud_itab__350 = {
          /* 00 */        1760,     INVALID,
        };

        internal static readonly ushort[] ud_itab__351 = {
          /* 00 */         875,         877,         915,
        };

        internal static readonly ushort[] ud_itab__352 = {
          /* 00 */        1085,     INVALID,
        };

        internal static readonly ushort[] ud_itab__353 = {
          /* 00 */        1755,     INVALID,
        };

        internal static readonly ushort[] ud_itab__354 = {
          /* 00 */        1757,     INVALID,
        };

        internal static readonly ushort[] ud_itab__355 = {
          /* 00 */        1759,     INVALID,
        };

        internal static readonly ushort[] ud_itab__356 = {
          /* 00 */     INVALID,        1520,
        };

        internal static readonly ushort[] ud_itab__357 = {
          /* 00 */        1584,        1587,        1590,        1593,
          /* 04 */        1596,        1599,        1602,        1605,
          /* 08 */        1608,        1614,        1611,        1617,
          /* 0c */  0x8000|358,  0x8000|359,  0x8000|360,  0x8000|361,
          /* 10 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 14 */     INVALID,     INVALID,     INVALID,        1712,
          /* 18 */  0x8000|362,  0x8000|363,     INVALID,     INVALID,
          /* 1c */        1575,        1578,        1581,     INVALID,
          /* 20 */        1686,        1688,        1690,        1692,
          /* 24 */        1694,     INVALID,     INVALID,     INVALID,
          /* 28 */        1623,        1709,        1682,        1684,
          /* 2c */  0x8000|365,  0x8000|366,  0x8000|367,  0x8000|368,
          /* 30 */        1697,        1699,        1701,        1703,
          /* 34 */        1705,        1707,     INVALID,        1718,
          /* 38 */        1625,        1627,        1629,        1631,
          /* 3c */        1633,        1635,        1639,        1637,
          /* 40 */        1641,        1643,     INVALID,     INVALID,
          /* 44 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 48 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 4c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 50 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 54 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 58 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 5c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 60 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 64 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 68 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 6c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 70 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 74 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 78 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 7c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 80 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 84 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 88 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 8c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 90 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 94 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 98 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 9c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* a0 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* a4 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* a8 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* ac */     INVALID,     INVALID,     INVALID,     INVALID,
          /* b0 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* b4 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* b8 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* bc */     INVALID,     INVALID,     INVALID,     INVALID,
          /* c0 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* c4 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* c8 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* cc */     INVALID,     INVALID,     INVALID,     INVALID,
          /* d0 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* d4 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* d8 */     INVALID,     INVALID,     INVALID,          46,
          /* dc */          42,          44,          38,          40,
          /* e0 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* e4 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* e8 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* ec */     INVALID,     INVALID,     INVALID,     INVALID,
          /* f0 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* f4 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* f8 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* fc */     INVALID,     INVALID,     INVALID,     INVALID,
        };

        internal static readonly ushort[] ud_itab__358 = {
          /* 00 */        1737,     INVALID,
        };

        internal static readonly ushort[] ud_itab__359 = {
          /* 00 */        1735,     INVALID,
        };

        internal static readonly ushort[] ud_itab__360 = {
          /* 00 */        1740,     INVALID,
        };

        internal static readonly ushort[] ud_itab__361 = {
          /* 00 */        1741,     INVALID,
        };

        internal static readonly ushort[] ud_itab__362 = {
          /* 00 */        1727,     INVALID,
        };

        internal static readonly ushort[] ud_itab__363 = {
          /* 00 */  0x8000|364,     INVALID,
        };

        internal static readonly ushort[] ud_itab__364 = {
          /* 00 */     INVALID,        1728,
        };

        internal static readonly ushort[] ud_itab__365 = {
          /* 00 */        1731,     INVALID,
        };

        internal static readonly ushort[] ud_itab__366 = {
          /* 00 */        1733,     INVALID,
        };

        internal static readonly ushort[] ud_itab__367 = {
          /* 00 */        1732,     INVALID,
        };

        internal static readonly ushort[] ud_itab__368 = {
          /* 00 */        1734,     INVALID,
        };

        internal static readonly ushort[] ud_itab__369 = {
          /* 00 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 04 */  0x8000|370,  0x8000|371,  0x8000|372,     INVALID,
          /* 08 */        1645,        1647,        1649,        1651,
          /* 0c */        1655,        1653,        1678,        1620,
          /* 10 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 14 */  0x8000|374,        1057,  0x8000|375,         202,
          /* 18 */  0x8000|379,  0x8000|381,     INVALID,     INVALID,
          /* 1c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 20 */  0x8000|383,        1558,  0x8000|385,     INVALID,
          /* 24 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 28 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 2c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 30 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 34 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 38 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 3c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 40 */         198,         196,        1680,     INVALID,
          /* 44 */        1513,     INVALID,     INVALID,     INVALID,
          /* 48 */     INVALID,     INVALID,  0x8000|391,  0x8000|392,
          /* 4c */  0x8000|393,     INVALID,     INVALID,     INVALID,
          /* 50 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 54 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 58 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 5c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 60 */        1716,        1714,        1722,        1720,
          /* 64 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 68 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 6c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 70 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 74 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 78 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 7c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 80 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 84 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 88 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 8c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 90 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 94 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 98 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 9c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* a0 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* a4 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* a8 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* ac */     INVALID,     INVALID,     INVALID,     INVALID,
          /* b0 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* b4 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* b8 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* bc */     INVALID,     INVALID,     INVALID,     INVALID,
          /* c0 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* c4 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* c8 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* cc */     INVALID,     INVALID,     INVALID,     INVALID,
          /* d0 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* d4 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* d8 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* dc */     INVALID,     INVALID,     INVALID,          48,
          /* e0 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* e4 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* e8 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* ec */     INVALID,     INVALID,     INVALID,     INVALID,
          /* f0 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* f4 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* f8 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* fc */     INVALID,     INVALID,     INVALID,     INVALID,
        };

        internal static readonly ushort[] ud_itab__370 = {
          /* 00 */        1738,     INVALID,
        };

        internal static readonly ushort[] ud_itab__371 = {
          /* 00 */        1736,     INVALID,
        };

        internal static readonly ushort[] ud_itab__372 = {
          /* 00 */  0x8000|373,     INVALID,
        };

        internal static readonly ushort[] ud_itab__373 = {
          /* 00 */     INVALID,        1739,
        };

        internal static readonly ushort[] ud_itab__374 = {
          /* 00 */        1046,     INVALID,
        };

        internal static readonly ushort[] ud_itab__375 = {
          /* 00 */  0x8000|376,  0x8000|377,  0x8000|378,
        };

        internal static readonly ushort[] ud_itab__376 = {
          /* 00 */        1048,     INVALID,
        };

        internal static readonly ushort[] ud_itab__377 = {
          /* 00 */        1050,     INVALID,
        };

        internal static readonly ushort[] ud_itab__378 = {
          /* 00 */     INVALID,        1052,
        };

        internal static readonly ushort[] ud_itab__379 = {
          /* 00 */  0x8000|380,     INVALID,
        };

        internal static readonly ushort[] ud_itab__380 = {
          /* 00 */     INVALID,        1730,
        };

        internal static readonly ushort[] ud_itab__381 = {
          /* 00 */  0x8000|382,     INVALID,
        };

        internal static readonly ushort[] ud_itab__382 = {
          /* 00 */     INVALID,        1729,
        };

        internal static readonly ushort[] ud_itab__383 = {
          /* 00 */  0x8000|384,     INVALID,
        };

        internal static readonly ushort[] ud_itab__384 = {
          /* 00 */        1065,     INVALID,
        };

        internal static readonly ushort[] ud_itab__385 = {
          /* 00 */  0x8000|386,  0x8000|388,
        };

        internal static readonly ushort[] ud_itab__386 = {
          /* 00 */  0x8000|387,     INVALID,
        };

        internal static readonly ushort[] ud_itab__387 = {
          /* 00 */        1066,     INVALID,
        };

        internal static readonly ushort[] ud_itab__388 = {
          /* 00 */  0x8000|389,  0x8000|390,
        };

        internal static readonly ushort[] ud_itab__389 = {
          /* 00 */        1067,     INVALID,
        };

        internal static readonly ushort[] ud_itab__390 = {
          /* 00 */        1068,     INVALID,
        };

        internal static readonly ushort[] ud_itab__391 = {
          /* 00 */        1745,     INVALID,
        };

        internal static readonly ushort[] ud_itab__392 = {
          /* 00 */        1744,     INVALID,
        };

        internal static readonly ushort[] ud_itab__393 = {
          /* 00 */        1754,     INVALID,
        };

        internal static readonly ushort[] ud_itab__394 = {
          /* 00 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 04 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 08 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 0c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 10 */  0x8000|395,  0x8000|396,  0x8000|397,     INVALID,
          /* 14 */     INVALID,     INVALID,  0x8000|398,     INVALID,
          /* 18 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 1c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 20 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 24 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 28 */     INVALID,     INVALID,         155,     INVALID,
          /* 2c */         169,         159,     INVALID,     INVALID,
          /* 30 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 34 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 38 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 3c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 40 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 44 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 48 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 4c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 50 */     INVALID,        1394,        1309,        1294,
          /* 54 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 58 */          32,         951,         157,         164,
          /* 5c */        1424,         823,         194,         807,
          /* 60 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 64 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 68 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 6c */     INVALID,     INVALID,     INVALID,        1523,
          /* 70 */        1536,     INVALID,     INVALID,     INVALID,
          /* 74 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 78 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 7c */     INVALID,     INVALID,         917,        1525,
          /* 80 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 84 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 88 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 8c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 90 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 94 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 98 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 9c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* a0 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* a4 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* a8 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* ac */     INVALID,     INVALID,     INVALID,     INVALID,
          /* b0 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* b4 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* b8 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* bc */     INVALID,     INVALID,     INVALID,     INVALID,
          /* c0 */     INVALID,     INVALID,         121,     INVALID,
          /* c4 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* c8 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* cc */     INVALID,     INVALID,     INVALID,     INVALID,
          /* d0 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* d4 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* d8 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* dc */     INVALID,     INVALID,     INVALID,     INVALID,
          /* e0 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* e4 */     INVALID,     INVALID,         133,     INVALID,
          /* e8 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* ec */     INVALID,     INVALID,     INVALID,     INVALID,
          /* f0 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* f4 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* f8 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* fc */     INVALID,     INVALID,     INVALID,     INVALID,
        };

        internal static readonly ushort[] ud_itab__395 = {
          /* 00 */        1751,        1750,
        };

        internal static readonly ushort[] ud_itab__396 = {
          /* 00 */        1753,        1752,
        };

        internal static readonly ushort[] ud_itab__397 = {
          /* 00 */        1572,        1570,
        };

        internal static readonly ushort[] ud_itab__398 = {
          /* 00 */        1568,        1566,
        };

        internal static readonly ushort[] ud_itab__399 = {
          /* 00 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 04 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 08 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 0c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 10 */  0x8000|400,  0x8000|401,  0x8000|402,     INVALID,
          /* 14 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 18 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 1c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 20 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 24 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 28 */     INVALID,     INVALID,         153,     INVALID,
          /* 2c */         167,         149,     INVALID,     INVALID,
          /* 30 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 34 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 38 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 3c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 40 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 44 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 48 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 4c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 50 */     INVALID,        1392,     INVALID,     INVALID,
          /* 54 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 58 */          30,         949,         151,     INVALID,
          /* 5c */        1422,         821,         192,         805,
          /* 60 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 64 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 68 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 6c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 70 */        1538,     INVALID,     INVALID,     INVALID,
          /* 74 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 78 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 7c */        1552,        1556,     INVALID,     INVALID,
          /* 80 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 84 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 88 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 8c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 90 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 94 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 98 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 9c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* a0 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* a4 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* a8 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* ac */     INVALID,     INVALID,     INVALID,     INVALID,
          /* b0 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* b4 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* b8 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* bc */     INVALID,     INVALID,     INVALID,     INVALID,
          /* c0 */     INVALID,     INVALID,         118,     INVALID,
          /* c4 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* c8 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* cc */     INVALID,     INVALID,     INVALID,     INVALID,
          /* d0 */          36,     INVALID,     INVALID,     INVALID,
          /* d4 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* d8 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* dc */     INVALID,     INVALID,     INVALID,     INVALID,
          /* e0 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* e4 */     INVALID,     INVALID,         137,     INVALID,
          /* e8 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* ec */     INVALID,     INVALID,     INVALID,     INVALID,
          /* f0 */        1560,     INVALID,     INVALID,     INVALID,
          /* f4 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* f8 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* fc */     INVALID,     INVALID,     INVALID,     INVALID,
        };

        internal static readonly ushort[] ud_itab__400 = {
          /* 00 */        1747,        1746,
        };

        internal static readonly ushort[] ud_itab__401 = {
          /* 00 */        1749,        1748,
        };

        internal static readonly ushort[] ud_itab__402 = {
          /* 00 */        1564,        1562,
        };

        internal static readonly ushort[] ud_itab__403 = {
          /* 00 */  0x8000|404,  0x8000|335,     INVALID,     INVALID,
          /* 04 */     INVALID,  0x8000|341,  0x8000|357,  0x8000|369,
          /* 08 */     INVALID,  0x8000|394,     INVALID,     INVALID,
          /* 0c */     INVALID,  0x8000|399,     INVALID,     INVALID,
        };

        internal static readonly ushort[] ud_itab__404 = {
          /* 00 */         769,     INVALID,
        };

        internal static readonly ushort[] ud_itab__405 = {
          /* 00 */         826,     INVALID,     INVALID,     INVALID,
          /* 04 */     INVALID,     INVALID,     INVALID,     INVALID,
        };

        internal static readonly ushort[] ud_itab__406 = {
          /* 00 */         827,     INVALID,     INVALID,     INVALID,
          /* 04 */     INVALID,     INVALID,     INVALID,     INVALID,
        };

        internal static readonly ushort[] ud_itab__407 = {
          /* 00 */         715,     INVALID,
        };

        internal static readonly ushort[] ud_itab__408 = {
          /* 00 */         723,         724,         725,
        };

        internal static readonly ushort[] ud_itab__409 = {
          /* 00 */        1280,        1285,        1269,        1273,
          /* 04 */        1326,        1333,        1320,        1314,
        };

        internal static readonly ushort[] ud_itab__410 = {
          /* 00 */        1281,        1288,        1272,        1276,
          /* 04 */        1325,        1332,        1329,        1312,
        };

        internal static readonly ushort[] ud_itab__411 = {
          /* 00 */        1282,        1289,        1270,        1277,
          /* 04 */        1324,        1331,        1321,        1316,
        };

        internal static readonly ushort[] ud_itab__412 = {
          /* 00 */        1283,        1290,        1271,        1278,
          /* 04 */        1328,        1335,        1322,        1317,
        };

        internal static readonly ushort[] ud_itab__413 = {
          /* 00 */           3,     INVALID,
        };

        internal static readonly ushort[] ud_itab__414 = {
          /* 00 */           2,     INVALID,
        };

        internal static readonly ushort[] ud_itab__415 = {
          /* 00 */        1311,     INVALID,
        };

        internal static readonly ushort[] ud_itab__416 = {
          /* 00 */  0x8000|417,  0x8000|418,
        };

        internal static readonly ushort[] ud_itab__417 = {
          /* 00 */         206,         503,         307,         357,
          /* 04 */         587,         630,         387,         413,
        };

        internal static readonly ushort[] ud_itab__418 = {
          /* 00 */         215,         216,         217,         218,
          /* 04 */         219,         220,         221,         222,
          /* 08 */         504,         505,         506,         507,
          /* 0c */         508,         509,         510,         511,
          /* 10 */         309,         310,         311,         312,
          /* 14 */         313,         314,         315,         316,
          /* 18 */         359,         360,         361,         362,
          /* 1c */         363,         364,         365,         366,
          /* 20 */         589,         590,         591,         592,
          /* 24 */         593,         594,         595,         596,
          /* 28 */         614,         615,         616,         617,
          /* 2c */         618,         619,         620,         621,
          /* 30 */         388,         389,         390,         391,
          /* 34 */         392,         393,         394,         395,
          /* 38 */         414,         415,         416,         417,
          /* 3c */         418,         419,         420,         421,
        };

        internal static readonly ushort[] ud_itab__419 = {
          /* 00 */  0x8000|420,  0x8000|421,
        };

        internal static readonly ushort[] ud_itab__420 = {
          /* 00 */         476,     INVALID,         573,         540,
          /* 04 */         493,         492,         584,         583,
        };

        internal static readonly ushort[] ud_itab__421 = {
          /* 00 */         477,         478,         479,         480,
          /* 04 */         481,         482,         483,         484,
          /* 08 */         658,         659,         660,         661,
          /* 0c */         662,         663,         664,         665,
          /* 10 */         522,     INVALID,     INVALID,     INVALID,
          /* 14 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 18 */         549,         550,         551,         552,
          /* 1c */         553,         554,         555,         556,
          /* 20 */         233,         204,     INVALID,     INVALID,
          /* 24 */         639,         657,     INVALID,     INVALID,
          /* 28 */         485,         486,         487,         488,
          /* 2c */         489,         490,         491,     INVALID,
          /* 30 */         203,         685,         529,         526,
          /* 34 */         684,         528,         377,         454,
          /* 38 */         527,         686,         537,         536,
          /* 3c */         530,         534,         535,         376,
        };

        internal static readonly ushort[] ud_itab__422 = {
          /* 00 */  0x8000|423,  0x8000|424,
        };

        internal static readonly ushort[] ud_itab__423 = {
          /* 00 */         456,         520,         448,         450,
          /* 04 */         462,         464,         460,         458,
        };

        internal static readonly ushort[] ud_itab__424 = {
          /* 00 */         235,         236,         237,         238,
          /* 04 */         239,         240,         241,         242,
          /* 08 */         243,         244,         245,         246,
          /* 0c */         247,         248,         249,         250,
          /* 10 */         251,         252,         253,         254,
          /* 14 */         255,         256,         257,         258,
          /* 18 */         259,         260,         261,         262,
          /* 1c */         263,         264,         265,         266,
          /* 20 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 24 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 28 */     INVALID,         656,     INVALID,     INVALID,
          /* 2c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 30 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 34 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 38 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 3c */     INVALID,     INVALID,     INVALID,     INVALID,
        };

        internal static readonly ushort[] ud_itab__425 = {
          /* 00 */  0x8000|426,  0x8000|427,
        };

        internal static readonly ushort[] ud_itab__426 = {
          /* 00 */         453,         471,         467,         470,
          /* 04 */     INVALID,         474,     INVALID,         538,
        };

        internal static readonly ushort[] ud_itab__427 = {
          /* 00 */         267,         268,         269,         270,
          /* 04 */         271,         272,         273,         274,
          /* 08 */         275,         276,         277,         278,
          /* 0c */         279,         280,         281,         282,
          /* 10 */         283,         284,         285,         286,
          /* 14 */         287,         288,         289,         290,
          /* 18 */         291,         292,         293,         294,
          /* 1c */         295,         296,         297,         298,
          /* 20 */         524,         523,         234,         455,
          /* 24 */         525,         532,     INVALID,     INVALID,
          /* 28 */         299,         300,         301,         302,
          /* 2c */         303,         304,         305,         306,
          /* 30 */         333,         334,         335,         336,
          /* 34 */         337,         338,         339,         340,
          /* 38 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 3c */     INVALID,     INVALID,     INVALID,     INVALID,
        };

        internal static readonly ushort[] ud_itab__428 = {
          /* 00 */  0x8000|429,  0x8000|430,
        };

        internal static readonly ushort[] ud_itab__429 = {
          /* 00 */         205,         494,         308,         358,
          /* 04 */         588,         613,         378,         404,
        };

        internal static readonly ushort[] ud_itab__430 = {
          /* 00 */         207,         208,         209,         210,
          /* 04 */         211,         212,         213,         214,
          /* 08 */         495,         496,         497,         498,
          /* 0c */         499,         500,         501,         502,
          /* 10 */         317,         318,         319,         320,
          /* 14 */         321,         322,         323,         324,
          /* 18 */         325,         326,         327,         328,
          /* 1c */         329,         330,         331,         332,
          /* 20 */         622,         623,         624,         625,
          /* 24 */         626,         627,         628,         629,
          /* 28 */         597,         598,         599,         600,
          /* 2c */         601,         602,         603,         604,
          /* 30 */         405,         406,         407,         408,
          /* 34 */         409,         410,         411,         412,
          /* 38 */         379,         380,         381,         382,
          /* 3c */         383,         384,         385,         386,
        };

        internal static readonly ushort[] ud_itab__431 = {
          /* 00 */  0x8000|432,  0x8000|433,
        };

        internal static readonly ushort[] ud_itab__432 = {
          /* 00 */         475,         472,         574,         539,
          /* 04 */         531,     INVALID,         533,         585,
        };

        internal static readonly ushort[] ud_itab__433 = {
          /* 00 */         431,         432,         433,         434,
          /* 04 */         435,         436,         437,         438,
          /* 08 */         666,         667,         668,         669,
          /* 0c */         670,         671,         672,         673,
          /* 10 */         575,         576,         577,         578,
          /* 14 */         579,         580,         581,         582,
          /* 18 */         541,         542,         543,         544,
          /* 1c */         545,         546,         547,         548,
          /* 20 */         640,         641,         642,         643,
          /* 24 */         644,         645,         646,         647,
          /* 28 */         648,         649,         650,         651,
          /* 2c */         652,         653,         654,         655,
          /* 30 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 34 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 38 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 3c */     INVALID,     INVALID,     INVALID,     INVALID,
        };

        internal static readonly ushort[] ud_itab__434 = {
          /* 00 */  0x8000|435,  0x8000|436,
        };

        internal static readonly ushort[] ud_itab__435 = {
          /* 00 */         457,         521,         447,         449,
          /* 04 */         463,         465,         461,         459,
        };

        internal static readonly ushort[] ud_itab__436 = {
          /* 00 */         223,         224,         225,         226,
          /* 04 */         227,         228,         229,         230,
          /* 08 */         512,         513,         514,         515,
          /* 0c */         516,         517,         518,         519,
          /* 10 */         367,         368,         369,         370,
          /* 14 */         371,         372,         373,         374,
          /* 18 */     INVALID,         375,     INVALID,     INVALID,
          /* 1c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 20 */         631,         632,         633,         634,
          /* 24 */         635,         636,         637,         638,
          /* 28 */         605,         606,         607,         608,
          /* 2c */         609,         610,         611,         612,
          /* 30 */         422,         423,         424,         425,
          /* 34 */         426,         427,         428,         429,
          /* 38 */         396,         397,         398,         399,
          /* 3c */         400,         401,         402,         403,
        };

        internal static readonly ushort[] ud_itab__437 = {
          /* 00 */  0x8000|438,  0x8000|439,
        };

        internal static readonly ushort[] ud_itab__438 = {
          /* 00 */         451,         473,         466,         468,
          /* 04 */         231,         452,         232,         469,
        };

        internal static readonly ushort[] ud_itab__439 = {
          /* 00 */         439,         440,         441,         442,
          /* 04 */         443,         444,         445,         446,
          /* 08 */         674,         675,         676,         677,
          /* 0c */         678,         679,         680,         681,
          /* 10 */         557,         558,         559,         560,
          /* 14 */         561,         562,         563,         564,
          /* 18 */         565,         566,         567,         568,
          /* 1c */         569,         570,         571,         572,
          /* 20 */         586,     INVALID,     INVALID,     INVALID,
          /* 24 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 28 */         341,         342,         343,         344,
          /* 2c */         345,         346,         347,         348,
          /* 30 */         349,         350,         351,         352,
          /* 34 */         353,         354,         355,         356,
          /* 38 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 3c */     INVALID,     INVALID,     INVALID,     INVALID,
        };

        internal static readonly ushort[] ud_itab__440 = {
          /* 00 */         758,         759,         760,
        };

        internal static readonly ushort[] ud_itab__441 = {
          /* 00 */         764,     INVALID,
        };

        internal static readonly ushort[] ud_itab__442 = {
          /* 00 */        1432,        1437,         962,         953,
          /* 04 */         942,         695,         186,         689,
        };

        internal static readonly ushort[] ud_itab__443 = {
          /* 00 */        1438,        1439,         963,         954,
          /* 04 */         943,         696,         185,         688,
        };

        internal static readonly ushort[] ud_itab__444 = {
          /* 00 */         708,         183,     INVALID,     INVALID,
          /* 04 */     INVALID,     INVALID,     INVALID,     INVALID,
        };

        internal static readonly ushort[] ud_itab__445 = {
          /* 00 */         707,         184,  0x8000|446,          71,
          /* 04 */         761,         762,        1255,     INVALID,
        };

        internal static readonly ushort[] ud_itab__446 = {
          /* 00 */          69,          70,
        };
        #endregion

        #region Lookup Table List

        internal static readonly UdLookupTableListEntry[] ud_lookup_table_list = new UdLookupTableListEntry[] {
            /* 000 */ new UdLookupTableListEntry( ud_itab__0, UdTableType.UD_TAB__OPC_TABLE, "opctbl" ),
            /* 001 */ new UdLookupTableListEntry( ud_itab__1, UdTableType.UD_TAB__OPC_MODE, "/m" ),
            /* 002 */ new UdLookupTableListEntry( ud_itab__2, UdTableType.UD_TAB__OPC_MODE, "/m" ),
            /* 003 */ new UdLookupTableListEntry( ud_itab__3, UdTableType.UD_TAB__OPC_MODE, "/m" ),
            /* 004 */ new UdLookupTableListEntry( ud_itab__4, UdTableType.UD_TAB__OPC_TABLE, "opctbl" ),
            /* 005 */ new UdLookupTableListEntry( ud_itab__5, UdTableType.UD_TAB__OPC_REG, "/reg" ),
            /* 006 */ new UdLookupTableListEntry( ud_itab__6, UdTableType.UD_TAB__OPC_MOD, "/mod" ),
            /* 007 */ new UdLookupTableListEntry( ud_itab__7, UdTableType.UD_TAB__OPC_REG, "/reg" ),
            /* 008 */ new UdLookupTableListEntry( ud_itab__8, UdTableType.UD_TAB__OPC_REG, "/reg" ),
            /* 009 */ new UdLookupTableListEntry( ud_itab__9, UdTableType.UD_TAB__OPC_RM, "/rm" ),
            /* 010 */ new UdLookupTableListEntry( ud_itab__10, UdTableType.UD_TAB__OPC_VENDOR, "/vendor" ),
            /* 011 */ new UdLookupTableListEntry( ud_itab__11, UdTableType.UD_TAB__OPC_VENDOR, "/vendor" ),
            /* 012 */ new UdLookupTableListEntry( ud_itab__12, UdTableType.UD_TAB__OPC_VENDOR, "/vendor" ),
            /* 013 */ new UdLookupTableListEntry( ud_itab__13, UdTableType.UD_TAB__OPC_VENDOR, "/vendor" ),
            /* 014 */ new UdLookupTableListEntry( ud_itab__14, UdTableType.UD_TAB__OPC_RM, "/rm" ),
            /* 015 */ new UdLookupTableListEntry( ud_itab__15, UdTableType.UD_TAB__OPC_RM, "/rm" ),
            /* 016 */ new UdLookupTableListEntry( ud_itab__16, UdTableType.UD_TAB__OPC_RM, "/rm" ),
            /* 017 */ new UdLookupTableListEntry( ud_itab__17, UdTableType.UD_TAB__OPC_VENDOR, "/vendor" ),
            /* 018 */ new UdLookupTableListEntry( ud_itab__18, UdTableType.UD_TAB__OPC_VENDOR, "/vendor" ),
            /* 019 */ new UdLookupTableListEntry( ud_itab__19, UdTableType.UD_TAB__OPC_VENDOR, "/vendor" ),
            /* 020 */ new UdLookupTableListEntry( ud_itab__20, UdTableType.UD_TAB__OPC_VENDOR, "/vendor" ),
            /* 021 */ new UdLookupTableListEntry( ud_itab__21, UdTableType.UD_TAB__OPC_VENDOR, "/vendor" ),
            /* 022 */ new UdLookupTableListEntry( ud_itab__22, UdTableType.UD_TAB__OPC_VENDOR, "/vendor" ),
            /* 023 */ new UdLookupTableListEntry( ud_itab__23, UdTableType.UD_TAB__OPC_VENDOR, "/vendor" ),
            /* 024 */ new UdLookupTableListEntry( ud_itab__24, UdTableType.UD_TAB__OPC_VENDOR, "/vendor" ),
            /* 025 */ new UdLookupTableListEntry( ud_itab__25, UdTableType.UD_TAB__OPC_RM, "/rm" ),
            /* 026 */ new UdLookupTableListEntry( ud_itab__26, UdTableType.UD_TAB__OPC_VENDOR, "/vendor" ),
            /* 027 */ new UdLookupTableListEntry( ud_itab__27, UdTableType.UD_TAB__OPC_REG, "/reg" ),
            /* 028 */ new UdLookupTableListEntry( ud_itab__28, UdTableType.UD_TAB__OPC_3DNOW, "/3dnow" ),
            /* 029 */ new UdLookupTableListEntry( ud_itab__29, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 030 */ new UdLookupTableListEntry( ud_itab__30, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 031 */ new UdLookupTableListEntry( ud_itab__31, UdTableType.UD_TAB__OPC_MOD, "/mod" ),
            /* 032 */ new UdLookupTableListEntry( ud_itab__32, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 033 */ new UdLookupTableListEntry( ud_itab__33, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 034 */ new UdLookupTableListEntry( ud_itab__34, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 035 */ new UdLookupTableListEntry( ud_itab__35, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 036 */ new UdLookupTableListEntry( ud_itab__36, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 037 */ new UdLookupTableListEntry( ud_itab__37, UdTableType.UD_TAB__OPC_MOD, "/mod" ),
            /* 038 */ new UdLookupTableListEntry( ud_itab__38, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 039 */ new UdLookupTableListEntry( ud_itab__39, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 040 */ new UdLookupTableListEntry( ud_itab__40, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 041 */ new UdLookupTableListEntry( ud_itab__41, UdTableType.UD_TAB__OPC_REG, "/reg" ),
            /* 042 */ new UdLookupTableListEntry( ud_itab__42, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 043 */ new UdLookupTableListEntry( ud_itab__43, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 044 */ new UdLookupTableListEntry( ud_itab__44, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 045 */ new UdLookupTableListEntry( ud_itab__45, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 046 */ new UdLookupTableListEntry( ud_itab__46, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 047 */ new UdLookupTableListEntry( ud_itab__47, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 048 */ new UdLookupTableListEntry( ud_itab__48, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 049 */ new UdLookupTableListEntry( ud_itab__49, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 050 */ new UdLookupTableListEntry( ud_itab__50, UdTableType.UD_TAB__OPC_MODE, "/m" ),
            /* 051 */ new UdLookupTableListEntry( ud_itab__51, UdTableType.UD_TAB__OPC_VENDOR, "/vendor" ),
            /* 052 */ new UdLookupTableListEntry( ud_itab__52, UdTableType.UD_TAB__OPC_MODE, "/m" ),
            /* 053 */ new UdLookupTableListEntry( ud_itab__53, UdTableType.UD_TAB__OPC_VENDOR, "/vendor" ),
            /* 054 */ new UdLookupTableListEntry( ud_itab__54, UdTableType.UD_TAB__OPC_TABLE, "opctbl" ),
            /* 055 */ new UdLookupTableListEntry( ud_itab__55, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 056 */ new UdLookupTableListEntry( ud_itab__56, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 057 */ new UdLookupTableListEntry( ud_itab__57, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 058 */ new UdLookupTableListEntry( ud_itab__58, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 059 */ new UdLookupTableListEntry( ud_itab__59, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 060 */ new UdLookupTableListEntry( ud_itab__60, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 061 */ new UdLookupTableListEntry( ud_itab__61, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 062 */ new UdLookupTableListEntry( ud_itab__62, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 063 */ new UdLookupTableListEntry( ud_itab__63, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 064 */ new UdLookupTableListEntry( ud_itab__64, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 065 */ new UdLookupTableListEntry( ud_itab__65, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 066 */ new UdLookupTableListEntry( ud_itab__66, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 067 */ new UdLookupTableListEntry( ud_itab__67, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 068 */ new UdLookupTableListEntry( ud_itab__68, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 069 */ new UdLookupTableListEntry( ud_itab__69, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 070 */ new UdLookupTableListEntry( ud_itab__70, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 071 */ new UdLookupTableListEntry( ud_itab__71, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 072 */ new UdLookupTableListEntry( ud_itab__72, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 073 */ new UdLookupTableListEntry( ud_itab__73, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 074 */ new UdLookupTableListEntry( ud_itab__74, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 075 */ new UdLookupTableListEntry( ud_itab__75, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 076 */ new UdLookupTableListEntry( ud_itab__76, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 077 */ new UdLookupTableListEntry( ud_itab__77, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 078 */ new UdLookupTableListEntry( ud_itab__78, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 079 */ new UdLookupTableListEntry( ud_itab__79, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 080 */ new UdLookupTableListEntry( ud_itab__80, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 081 */ new UdLookupTableListEntry( ud_itab__81, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 082 */ new UdLookupTableListEntry( ud_itab__82, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 083 */ new UdLookupTableListEntry( ud_itab__83, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 084 */ new UdLookupTableListEntry( ud_itab__84, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 085 */ new UdLookupTableListEntry( ud_itab__85, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 086 */ new UdLookupTableListEntry( ud_itab__86, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 087 */ new UdLookupTableListEntry( ud_itab__87, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 088 */ new UdLookupTableListEntry( ud_itab__88, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 089 */ new UdLookupTableListEntry( ud_itab__89, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 090 */ new UdLookupTableListEntry( ud_itab__90, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 091 */ new UdLookupTableListEntry( ud_itab__91, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 092 */ new UdLookupTableListEntry( ud_itab__92, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 093 */ new UdLookupTableListEntry( ud_itab__93, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 094 */ new UdLookupTableListEntry( ud_itab__94, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 095 */ new UdLookupTableListEntry( ud_itab__95, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 096 */ new UdLookupTableListEntry( ud_itab__96, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 097 */ new UdLookupTableListEntry( ud_itab__97, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 098 */ new UdLookupTableListEntry( ud_itab__98, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 099 */ new UdLookupTableListEntry( ud_itab__99, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 100 */ new UdLookupTableListEntry( ud_itab__100, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 101 */ new UdLookupTableListEntry( ud_itab__101, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 102 */ new UdLookupTableListEntry( ud_itab__102, UdTableType.UD_TAB__OPC_MODE, "/m" ),
            /* 103 */ new UdLookupTableListEntry( ud_itab__103, UdTableType.UD_TAB__OPC_VENDOR, "/vendor" ),
            /* 104 */ new UdLookupTableListEntry( ud_itab__104, UdTableType.UD_TAB__OPC_VENDOR, "/vendor" ),
            /* 105 */ new UdLookupTableListEntry( ud_itab__105, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 106 */ new UdLookupTableListEntry( ud_itab__106, UdTableType.UD_TAB__OPC_MODE, "/m" ),
            /* 107 */ new UdLookupTableListEntry( ud_itab__107, UdTableType.UD_TAB__OPC_VENDOR, "/vendor" ),
            /* 108 */ new UdLookupTableListEntry( ud_itab__108, UdTableType.UD_TAB__OPC_VENDOR, "/vendor" ),
            /* 109 */ new UdLookupTableListEntry( ud_itab__109, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 110 */ new UdLookupTableListEntry( ud_itab__110, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 111 */ new UdLookupTableListEntry( ud_itab__111, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 112 */ new UdLookupTableListEntry( ud_itab__112, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 113 */ new UdLookupTableListEntry( ud_itab__113, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 114 */ new UdLookupTableListEntry( ud_itab__114, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 115 */ new UdLookupTableListEntry( ud_itab__115, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 116 */ new UdLookupTableListEntry( ud_itab__116, UdTableType.UD_TAB__OPC_TABLE, "opctbl" ),
            /* 117 */ new UdLookupTableListEntry( ud_itab__117, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 118 */ new UdLookupTableListEntry( ud_itab__118, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 119 */ new UdLookupTableListEntry( ud_itab__119, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 120 */ new UdLookupTableListEntry( ud_itab__120, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 121 */ new UdLookupTableListEntry( ud_itab__121, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 122 */ new UdLookupTableListEntry( ud_itab__122, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 123 */ new UdLookupTableListEntry( ud_itab__123, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 124 */ new UdLookupTableListEntry( ud_itab__124, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 125 */ new UdLookupTableListEntry( ud_itab__125, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 126 */ new UdLookupTableListEntry( ud_itab__126, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 127 */ new UdLookupTableListEntry( ud_itab__127, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 128 */ new UdLookupTableListEntry( ud_itab__128, UdTableType.UD_TAB__OPC_OSIZE, "/o" ),
            /* 129 */ new UdLookupTableListEntry( ud_itab__129, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 130 */ new UdLookupTableListEntry( ud_itab__130, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 131 */ new UdLookupTableListEntry( ud_itab__131, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 132 */ new UdLookupTableListEntry( ud_itab__132, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 133 */ new UdLookupTableListEntry( ud_itab__133, UdTableType.UD_TAB__OPC_OSIZE, "/o" ),
            /* 134 */ new UdLookupTableListEntry( ud_itab__134, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 135 */ new UdLookupTableListEntry( ud_itab__135, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 136 */ new UdLookupTableListEntry( ud_itab__136, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 137 */ new UdLookupTableListEntry( ud_itab__137, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 138 */ new UdLookupTableListEntry( ud_itab__138, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 139 */ new UdLookupTableListEntry( ud_itab__139, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 140 */ new UdLookupTableListEntry( ud_itab__140, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 141 */ new UdLookupTableListEntry( ud_itab__141, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 142 */ new UdLookupTableListEntry( ud_itab__142, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 143 */ new UdLookupTableListEntry( ud_itab__143, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 144 */ new UdLookupTableListEntry( ud_itab__144, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 145 */ new UdLookupTableListEntry( ud_itab__145, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 146 */ new UdLookupTableListEntry( ud_itab__146, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 147 */ new UdLookupTableListEntry( ud_itab__147, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 148 */ new UdLookupTableListEntry( ud_itab__148, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 149 */ new UdLookupTableListEntry( ud_itab__149, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 150 */ new UdLookupTableListEntry( ud_itab__150, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 151 */ new UdLookupTableListEntry( ud_itab__151, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 152 */ new UdLookupTableListEntry( ud_itab__152, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 153 */ new UdLookupTableListEntry( ud_itab__153, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 154 */ new UdLookupTableListEntry( ud_itab__154, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 155 */ new UdLookupTableListEntry( ud_itab__155, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 156 */ new UdLookupTableListEntry( ud_itab__156, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 157 */ new UdLookupTableListEntry( ud_itab__157, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 158 */ new UdLookupTableListEntry( ud_itab__158, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 159 */ new UdLookupTableListEntry( ud_itab__159, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 160 */ new UdLookupTableListEntry( ud_itab__160, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 161 */ new UdLookupTableListEntry( ud_itab__161, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 162 */ new UdLookupTableListEntry( ud_itab__162, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 163 */ new UdLookupTableListEntry( ud_itab__163, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 164 */ new UdLookupTableListEntry( ud_itab__164, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 165 */ new UdLookupTableListEntry( ud_itab__165, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 166 */ new UdLookupTableListEntry( ud_itab__166, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 167 */ new UdLookupTableListEntry( ud_itab__167, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 168 */ new UdLookupTableListEntry( ud_itab__168, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 169 */ new UdLookupTableListEntry( ud_itab__169, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 170 */ new UdLookupTableListEntry( ud_itab__170, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 171 */ new UdLookupTableListEntry( ud_itab__171, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 172 */ new UdLookupTableListEntry( ud_itab__172, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 173 */ new UdLookupTableListEntry( ud_itab__173, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 174 */ new UdLookupTableListEntry( ud_itab__174, UdTableType.UD_TAB__OPC_OSIZE, "/o" ),
            /* 175 */ new UdLookupTableListEntry( ud_itab__175, UdTableType.UD_TAB__OPC_OSIZE, "/o" ),
            /* 176 */ new UdLookupTableListEntry( ud_itab__176, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 177 */ new UdLookupTableListEntry( ud_itab__177, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 178 */ new UdLookupTableListEntry( ud_itab__178, UdTableType.UD_TAB__OPC_REG, "/reg" ),
            /* 179 */ new UdLookupTableListEntry( ud_itab__179, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 180 */ new UdLookupTableListEntry( ud_itab__180, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 181 */ new UdLookupTableListEntry( ud_itab__181, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 182 */ new UdLookupTableListEntry( ud_itab__182, UdTableType.UD_TAB__OPC_REG, "/reg" ),
            /* 183 */ new UdLookupTableListEntry( ud_itab__183, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 184 */ new UdLookupTableListEntry( ud_itab__184, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 185 */ new UdLookupTableListEntry( ud_itab__185, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 186 */ new UdLookupTableListEntry( ud_itab__186, UdTableType.UD_TAB__OPC_REG, "/reg" ),
            /* 187 */ new UdLookupTableListEntry( ud_itab__187, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 188 */ new UdLookupTableListEntry( ud_itab__188, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 189 */ new UdLookupTableListEntry( ud_itab__189, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 190 */ new UdLookupTableListEntry( ud_itab__190, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 191 */ new UdLookupTableListEntry( ud_itab__191, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 192 */ new UdLookupTableListEntry( ud_itab__192, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 193 */ new UdLookupTableListEntry( ud_itab__193, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 194 */ new UdLookupTableListEntry( ud_itab__194, UdTableType.UD_TAB__OPC_VENDOR, "/vendor" ),
            /* 195 */ new UdLookupTableListEntry( ud_itab__195, UdTableType.UD_TAB__OPC_VENDOR, "/vendor" ),
            /* 196 */ new UdLookupTableListEntry( ud_itab__196, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 197 */ new UdLookupTableListEntry( ud_itab__197, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 198 */ new UdLookupTableListEntry( ud_itab__198, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 199 */ new UdLookupTableListEntry( ud_itab__199, UdTableType.UD_TAB__OPC_OSIZE, "/o" ),
            /* 200 */ new UdLookupTableListEntry( ud_itab__200, UdTableType.UD_TAB__OPC_OSIZE, "/o" ),
            /* 201 */ new UdLookupTableListEntry( ud_itab__201, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 202 */ new UdLookupTableListEntry( ud_itab__202, UdTableType.UD_TAB__OPC_MOD, "/mod" ),
            /* 203 */ new UdLookupTableListEntry( ud_itab__203, UdTableType.UD_TAB__OPC_REG, "/reg" ),
            /* 204 */ new UdLookupTableListEntry( ud_itab__204, UdTableType.UD_TAB__OPC_RM, "/rm" ),
            /* 205 */ new UdLookupTableListEntry( ud_itab__205, UdTableType.UD_TAB__OPC_RM, "/rm" ),
            /* 206 */ new UdLookupTableListEntry( ud_itab__206, UdTableType.UD_TAB__OPC_RM, "/rm" ),
            /* 207 */ new UdLookupTableListEntry( ud_itab__207, UdTableType.UD_TAB__OPC_MOD, "/mod" ),
            /* 208 */ new UdLookupTableListEntry( ud_itab__208, UdTableType.UD_TAB__OPC_REG, "/reg" ),
            /* 209 */ new UdLookupTableListEntry( ud_itab__209, UdTableType.UD_TAB__OPC_RM, "/rm" ),
            /* 210 */ new UdLookupTableListEntry( ud_itab__210, UdTableType.UD_TAB__OPC_RM, "/rm" ),
            /* 211 */ new UdLookupTableListEntry( ud_itab__211, UdTableType.UD_TAB__OPC_RM, "/rm" ),
            /* 212 */ new UdLookupTableListEntry( ud_itab__212, UdTableType.UD_TAB__OPC_RM, "/rm" ),
            /* 213 */ new UdLookupTableListEntry( ud_itab__213, UdTableType.UD_TAB__OPC_RM, "/rm" ),
            /* 214 */ new UdLookupTableListEntry( ud_itab__214, UdTableType.UD_TAB__OPC_RM, "/rm" ),
            /* 215 */ new UdLookupTableListEntry( ud_itab__215, UdTableType.UD_TAB__OPC_MOD, "/mod" ),
            /* 216 */ new UdLookupTableListEntry( ud_itab__216, UdTableType.UD_TAB__OPC_REG, "/reg" ),
            /* 217 */ new UdLookupTableListEntry( ud_itab__217, UdTableType.UD_TAB__OPC_REG, "/reg" ),
            /* 218 */ new UdLookupTableListEntry( ud_itab__218, UdTableType.UD_TAB__OPC_RM, "/rm" ),
            /* 219 */ new UdLookupTableListEntry( ud_itab__219, UdTableType.UD_TAB__OPC_RM, "/rm" ),
            /* 220 */ new UdLookupTableListEntry( ud_itab__220, UdTableType.UD_TAB__OPC_RM, "/rm" ),
            /* 221 */ new UdLookupTableListEntry( ud_itab__221, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 222 */ new UdLookupTableListEntry( ud_itab__222, UdTableType.UD_TAB__OPC_REG, "/reg" ),
            /* 223 */ new UdLookupTableListEntry( ud_itab__223, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 224 */ new UdLookupTableListEntry( ud_itab__224, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 225 */ new UdLookupTableListEntry( ud_itab__225, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 226 */ new UdLookupTableListEntry( ud_itab__226, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 227 */ new UdLookupTableListEntry( ud_itab__227, UdTableType.UD_TAB__OPC_MOD, "/mod" ),
            /* 228 */ new UdLookupTableListEntry( ud_itab__228, UdTableType.UD_TAB__OPC_REG, "/reg" ),
            /* 229 */ new UdLookupTableListEntry( ud_itab__229, UdTableType.UD_TAB__OPC_OSIZE, "/o" ),
            /* 230 */ new UdLookupTableListEntry( ud_itab__230, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 231 */ new UdLookupTableListEntry( ud_itab__231, UdTableType.UD_TAB__OPC_VENDOR, "/vendor" ),
            /* 232 */ new UdLookupTableListEntry( ud_itab__232, UdTableType.UD_TAB__OPC_VENDOR, "/vendor" ),
            /* 233 */ new UdLookupTableListEntry( ud_itab__233, UdTableType.UD_TAB__OPC_VENDOR, "/vendor" ),
            /* 234 */ new UdLookupTableListEntry( ud_itab__234, UdTableType.UD_TAB__OPC_VENDOR, "/vendor" ),
            /* 235 */ new UdLookupTableListEntry( ud_itab__235, UdTableType.UD_TAB__OPC_REG, "/reg" ),
            /* 236 */ new UdLookupTableListEntry( ud_itab__236, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 237 */ new UdLookupTableListEntry( ud_itab__237, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 238 */ new UdLookupTableListEntry( ud_itab__238, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 239 */ new UdLookupTableListEntry( ud_itab__239, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 240 */ new UdLookupTableListEntry( ud_itab__240, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 241 */ new UdLookupTableListEntry( ud_itab__241, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 242 */ new UdLookupTableListEntry( ud_itab__242, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 243 */ new UdLookupTableListEntry( ud_itab__243, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 244 */ new UdLookupTableListEntry( ud_itab__244, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 245 */ new UdLookupTableListEntry( ud_itab__245, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 246 */ new UdLookupTableListEntry( ud_itab__246, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 247 */ new UdLookupTableListEntry( ud_itab__247, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 248 */ new UdLookupTableListEntry( ud_itab__248, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 249 */ new UdLookupTableListEntry( ud_itab__249, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 250 */ new UdLookupTableListEntry( ud_itab__250, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 251 */ new UdLookupTableListEntry( ud_itab__251, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 252 */ new UdLookupTableListEntry( ud_itab__252, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 253 */ new UdLookupTableListEntry( ud_itab__253, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 254 */ new UdLookupTableListEntry( ud_itab__254, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 255 */ new UdLookupTableListEntry( ud_itab__255, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 256 */ new UdLookupTableListEntry( ud_itab__256, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 257 */ new UdLookupTableListEntry( ud_itab__257, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 258 */ new UdLookupTableListEntry( ud_itab__258, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 259 */ new UdLookupTableListEntry( ud_itab__259, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 260 */ new UdLookupTableListEntry( ud_itab__260, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 261 */ new UdLookupTableListEntry( ud_itab__261, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 262 */ new UdLookupTableListEntry( ud_itab__262, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 263 */ new UdLookupTableListEntry( ud_itab__263, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 264 */ new UdLookupTableListEntry( ud_itab__264, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 265 */ new UdLookupTableListEntry( ud_itab__265, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 266 */ new UdLookupTableListEntry( ud_itab__266, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 267 */ new UdLookupTableListEntry( ud_itab__267, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 268 */ new UdLookupTableListEntry( ud_itab__268, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 269 */ new UdLookupTableListEntry( ud_itab__269, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 270 */ new UdLookupTableListEntry( ud_itab__270, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 271 */ new UdLookupTableListEntry( ud_itab__271, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 272 */ new UdLookupTableListEntry( ud_itab__272, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 273 */ new UdLookupTableListEntry( ud_itab__273, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 274 */ new UdLookupTableListEntry( ud_itab__274, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 275 */ new UdLookupTableListEntry( ud_itab__275, UdTableType.UD_TAB__OPC_MOD, "/mod" ),
            /* 276 */ new UdLookupTableListEntry( ud_itab__276, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 277 */ new UdLookupTableListEntry( ud_itab__277, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 278 */ new UdLookupTableListEntry( ud_itab__278, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 279 */ new UdLookupTableListEntry( ud_itab__279, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 280 */ new UdLookupTableListEntry( ud_itab__280, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 281 */ new UdLookupTableListEntry( ud_itab__281, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 282 */ new UdLookupTableListEntry( ud_itab__282, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 283 */ new UdLookupTableListEntry( ud_itab__283, UdTableType.UD_TAB__OPC_SSE, "/sse" ),
            /* 284 */ new UdLookupTableListEntry( ud_itab__284, UdTableType.UD_TAB__OPC_MODE, "/m" ),
            /* 285 */ new UdLookupTableListEntry( ud_itab__285, UdTableType.UD_TAB__OPC_MODE, "/m" ),
            /* 286 */ new UdLookupTableListEntry( ud_itab__286, UdTableType.UD_TAB__OPC_MODE, "/m" ),
            /* 287 */ new UdLookupTableListEntry( ud_itab__287, UdTableType.UD_TAB__OPC_MODE, "/m" ),
            /* 288 */ new UdLookupTableListEntry( ud_itab__288, UdTableType.UD_TAB__OPC_MODE, "/m" ),
            /* 289 */ new UdLookupTableListEntry( ud_itab__289, UdTableType.UD_TAB__OPC_MODE, "/m" ),
            /* 290 */ new UdLookupTableListEntry( ud_itab__290, UdTableType.UD_TAB__OPC_MODE, "/m" ),
            /* 291 */ new UdLookupTableListEntry( ud_itab__291, UdTableType.UD_TAB__OPC_MODE, "/m" ),
            /* 292 */ new UdLookupTableListEntry( ud_itab__292, UdTableType.UD_TAB__OPC_OSIZE, "/o" ),
            /* 293 */ new UdLookupTableListEntry( ud_itab__293, UdTableType.UD_TAB__OPC_MODE, "/m" ),
            /* 294 */ new UdLookupTableListEntry( ud_itab__294, UdTableType.UD_TAB__OPC_MODE, "/m" ),
            /* 295 */ new UdLookupTableListEntry( ud_itab__295, UdTableType.UD_TAB__OPC_OSIZE, "/o" ),
            /* 296 */ new UdLookupTableListEntry( ud_itab__296, UdTableType.UD_TAB__OPC_MODE, "/m" ),
            /* 297 */ new UdLookupTableListEntry( ud_itab__297, UdTableType.UD_TAB__OPC_MODE, "/m" ),
            /* 298 */ new UdLookupTableListEntry( ud_itab__298, UdTableType.UD_TAB__OPC_MODE, "/m" ),
            /* 299 */ new UdLookupTableListEntry( ud_itab__299, UdTableType.UD_TAB__OPC_MODE, "/m" ),
            /* 300 */ new UdLookupTableListEntry( ud_itab__300, UdTableType.UD_TAB__OPC_OSIZE, "/o" ),
            /* 301 */ new UdLookupTableListEntry( ud_itab__301, UdTableType.UD_TAB__OPC_OSIZE, "/o" ),
            /* 302 */ new UdLookupTableListEntry( ud_itab__302, UdTableType.UD_TAB__OPC_REG, "/reg" ),
            /* 303 */ new UdLookupTableListEntry( ud_itab__303, UdTableType.UD_TAB__OPC_REG, "/reg" ),
            /* 304 */ new UdLookupTableListEntry( ud_itab__304, UdTableType.UD_TAB__OPC_REG, "/reg" ),
            /* 305 */ new UdLookupTableListEntry( ud_itab__305, UdTableType.UD_TAB__OPC_MODE, "/m" ),
            /* 306 */ new UdLookupTableListEntry( ud_itab__306, UdTableType.UD_TAB__OPC_MODE, "/m" ),
            /* 307 */ new UdLookupTableListEntry( ud_itab__307, UdTableType.UD_TAB__OPC_MODE, "/m" ),
            /* 308 */ new UdLookupTableListEntry( ud_itab__308, UdTableType.UD_TAB__OPC_MODE, "/m" ),
            /* 309 */ new UdLookupTableListEntry( ud_itab__309, UdTableType.UD_TAB__OPC_MODE, "/m" ),
            /* 310 */ new UdLookupTableListEntry( ud_itab__310, UdTableType.UD_TAB__OPC_MODE, "/m" ),
            /* 311 */ new UdLookupTableListEntry( ud_itab__311, UdTableType.UD_TAB__OPC_MODE, "/m" ),
            /* 312 */ new UdLookupTableListEntry( ud_itab__312, UdTableType.UD_TAB__OPC_MODE, "/m" ),
            /* 313 */ new UdLookupTableListEntry( ud_itab__313, UdTableType.UD_TAB__OPC_REG, "/reg" ),
            /* 314 */ new UdLookupTableListEntry( ud_itab__314, UdTableType.UD_TAB__OPC_REG, "/reg" ),
            /* 315 */ new UdLookupTableListEntry( ud_itab__315, UdTableType.UD_TAB__OPC_OSIZE, "/o" ),
            /* 316 */ new UdLookupTableListEntry( ud_itab__316, UdTableType.UD_TAB__OPC_OSIZE, "/o" ),
            /* 317 */ new UdLookupTableListEntry( ud_itab__317, UdTableType.UD_TAB__OPC_MODE, "/m" ),
            /* 318 */ new UdLookupTableListEntry( ud_itab__318, UdTableType.UD_TAB__OPC_OSIZE, "/o" ),
            /* 319 */ new UdLookupTableListEntry( ud_itab__319, UdTableType.UD_TAB__OPC_MODE, "/m" ),
            /* 320 */ new UdLookupTableListEntry( ud_itab__320, UdTableType.UD_TAB__OPC_MODE, "/m" ),
            /* 321 */ new UdLookupTableListEntry( ud_itab__321, UdTableType.UD_TAB__OPC_MODE, "/m" ),
            /* 322 */ new UdLookupTableListEntry( ud_itab__322, UdTableType.UD_TAB__OPC_OSIZE, "/o" ),
            /* 323 */ new UdLookupTableListEntry( ud_itab__323, UdTableType.UD_TAB__OPC_MODE, "/m" ),
            /* 324 */ new UdLookupTableListEntry( ud_itab__324, UdTableType.UD_TAB__OPC_MODE, "/m" ),
            /* 325 */ new UdLookupTableListEntry( ud_itab__325, UdTableType.UD_TAB__OPC_MODE, "/m" ),
            /* 326 */ new UdLookupTableListEntry( ud_itab__326, UdTableType.UD_TAB__OPC_OSIZE, "/o" ),
            /* 327 */ new UdLookupTableListEntry( ud_itab__327, UdTableType.UD_TAB__OPC_OSIZE, "/o" ),
            /* 328 */ new UdLookupTableListEntry( ud_itab__328, UdTableType.UD_TAB__OPC_OSIZE, "/o" ),
            /* 329 */ new UdLookupTableListEntry( ud_itab__329, UdTableType.UD_TAB__OPC_OSIZE, "/o" ),
            /* 330 */ new UdLookupTableListEntry( ud_itab__330, UdTableType.UD_TAB__OPC_OSIZE, "/o" ),
            /* 331 */ new UdLookupTableListEntry( ud_itab__331, UdTableType.UD_TAB__OPC_REG, "/reg" ),
            /* 332 */ new UdLookupTableListEntry( ud_itab__332, UdTableType.UD_TAB__OPC_REG, "/reg" ),
            /* 333 */ new UdLookupTableListEntry( ud_itab__333, UdTableType.UD_TAB__OPC_VEX, "/vex" ),
            /* 334 */ new UdLookupTableListEntry( ud_itab__334, UdTableType.UD_TAB__OPC_MODE, "/m" ),
            /* 335 */ new UdLookupTableListEntry( ud_itab__335, UdTableType.UD_TAB__OPC_TABLE, "opctbl" ),
            /* 336 */ new UdLookupTableListEntry( ud_itab__336, UdTableType.UD_TAB__OPC_MOD, "/mod" ),
            /* 337 */ new UdLookupTableListEntry( ud_itab__337, UdTableType.UD_TAB__OPC_MOD, "/mod" ),
            /* 338 */ new UdLookupTableListEntry( ud_itab__338, UdTableType.UD_TAB__OPC_VEX_L, "/vexl" ),
            /* 339 */ new UdLookupTableListEntry( ud_itab__339, UdTableType.UD_TAB__OPC_MOD, "/mod" ),
            /* 340 */ new UdLookupTableListEntry( ud_itab__340, UdTableType.UD_TAB__OPC_REG, "/reg" ),
            /* 341 */ new UdLookupTableListEntry( ud_itab__341, UdTableType.UD_TAB__OPC_TABLE, "opctbl" ),
            /* 342 */ new UdLookupTableListEntry( ud_itab__342, UdTableType.UD_TAB__OPC_MOD, "/mod" ),
            /* 343 */ new UdLookupTableListEntry( ud_itab__343, UdTableType.UD_TAB__OPC_MOD, "/mod" ),
            /* 344 */ new UdLookupTableListEntry( ud_itab__344, UdTableType.UD_TAB__OPC_OSIZE, "/o" ),
            /* 345 */ new UdLookupTableListEntry( ud_itab__345, UdTableType.UD_TAB__OPC_REG, "/reg" ),
            /* 346 */ new UdLookupTableListEntry( ud_itab__346, UdTableType.UD_TAB__OPC_VEX_L, "/vexl" ),
            /* 347 */ new UdLookupTableListEntry( ud_itab__347, UdTableType.UD_TAB__OPC_REG, "/reg" ),
            /* 348 */ new UdLookupTableListEntry( ud_itab__348, UdTableType.UD_TAB__OPC_VEX_L, "/vexl" ),
            /* 349 */ new UdLookupTableListEntry( ud_itab__349, UdTableType.UD_TAB__OPC_REG, "/reg" ),
            /* 350 */ new UdLookupTableListEntry( ud_itab__350, UdTableType.UD_TAB__OPC_VEX_L, "/vexl" ),
            /* 351 */ new UdLookupTableListEntry( ud_itab__351, UdTableType.UD_TAB__OPC_OSIZE, "/o" ),
            /* 352 */ new UdLookupTableListEntry( ud_itab__352, UdTableType.UD_TAB__OPC_VEX_L, "/vexl" ),
            /* 353 */ new UdLookupTableListEntry( ud_itab__353, UdTableType.UD_TAB__OPC_VEX_L, "/vexl" ),
            /* 354 */ new UdLookupTableListEntry( ud_itab__354, UdTableType.UD_TAB__OPC_VEX_L, "/vexl" ),
            /* 355 */ new UdLookupTableListEntry( ud_itab__355, UdTableType.UD_TAB__OPC_VEX_L, "/vexl" ),
            /* 356 */ new UdLookupTableListEntry( ud_itab__356, UdTableType.UD_TAB__OPC_MOD, "/mod" ),
            /* 357 */ new UdLookupTableListEntry( ud_itab__357, UdTableType.UD_TAB__OPC_TABLE, "opctbl" ),
            /* 358 */ new UdLookupTableListEntry( ud_itab__358, UdTableType.UD_TAB__OPC_VEX_W, "/vexw" ),
            /* 359 */ new UdLookupTableListEntry( ud_itab__359, UdTableType.UD_TAB__OPC_VEX_W, "/vexw" ),
            /* 360 */ new UdLookupTableListEntry( ud_itab__360, UdTableType.UD_TAB__OPC_VEX_W, "/vexw" ),
            /* 361 */ new UdLookupTableListEntry( ud_itab__361, UdTableType.UD_TAB__OPC_VEX_W, "/vexw" ),
            /* 362 */ new UdLookupTableListEntry( ud_itab__362, UdTableType.UD_TAB__OPC_VEX_W, "/vexw" ),
            /* 363 */ new UdLookupTableListEntry( ud_itab__363, UdTableType.UD_TAB__OPC_VEX_W, "/vexw" ),
            /* 364 */ new UdLookupTableListEntry( ud_itab__364, UdTableType.UD_TAB__OPC_VEX_L, "/vexl" ),
            /* 365 */ new UdLookupTableListEntry( ud_itab__365, UdTableType.UD_TAB__OPC_VEX_W, "/vexw" ),
            /* 366 */ new UdLookupTableListEntry( ud_itab__366, UdTableType.UD_TAB__OPC_VEX_W, "/vexw" ),
            /* 367 */ new UdLookupTableListEntry( ud_itab__367, UdTableType.UD_TAB__OPC_VEX_W, "/vexw" ),
            /* 368 */ new UdLookupTableListEntry( ud_itab__368, UdTableType.UD_TAB__OPC_VEX_W, "/vexw" ),
            /* 369 */ new UdLookupTableListEntry( ud_itab__369, UdTableType.UD_TAB__OPC_TABLE, "opctbl" ),
            /* 370 */ new UdLookupTableListEntry( ud_itab__370, UdTableType.UD_TAB__OPC_VEX_W, "/vexw" ),
            /* 371 */ new UdLookupTableListEntry( ud_itab__371, UdTableType.UD_TAB__OPC_VEX_W, "/vexw" ),
            /* 372 */ new UdLookupTableListEntry( ud_itab__372, UdTableType.UD_TAB__OPC_VEX_W, "/vexw" ),
            /* 373 */ new UdLookupTableListEntry( ud_itab__373, UdTableType.UD_TAB__OPC_VEX_L, "/vexl" ),
            /* 374 */ new UdLookupTableListEntry( ud_itab__374, UdTableType.UD_TAB__OPC_VEX_W, "/vexw" ),
            /* 375 */ new UdLookupTableListEntry( ud_itab__375, UdTableType.UD_TAB__OPC_OSIZE, "/o" ),
            /* 376 */ new UdLookupTableListEntry( ud_itab__376, UdTableType.UD_TAB__OPC_VEX_W, "/vexw" ),
            /* 377 */ new UdLookupTableListEntry( ud_itab__377, UdTableType.UD_TAB__OPC_VEX_W, "/vexw" ),
            /* 378 */ new UdLookupTableListEntry( ud_itab__378, UdTableType.UD_TAB__OPC_VEX_W, "/vexw" ),
            /* 379 */ new UdLookupTableListEntry( ud_itab__379, UdTableType.UD_TAB__OPC_VEX_W, "/vexw" ),
            /* 380 */ new UdLookupTableListEntry( ud_itab__380, UdTableType.UD_TAB__OPC_VEX_L, "/vexl" ),
            /* 381 */ new UdLookupTableListEntry( ud_itab__381, UdTableType.UD_TAB__OPC_VEX_W, "/vexw" ),
            /* 382 */ new UdLookupTableListEntry( ud_itab__382, UdTableType.UD_TAB__OPC_VEX_L, "/vexl" ),
            /* 383 */ new UdLookupTableListEntry( ud_itab__383, UdTableType.UD_TAB__OPC_VEX_W, "/vexw" ),
            /* 384 */ new UdLookupTableListEntry( ud_itab__384, UdTableType.UD_TAB__OPC_VEX_L, "/vexl" ),
            /* 385 */ new UdLookupTableListEntry( ud_itab__385, UdTableType.UD_TAB__OPC_MODE, "/m" ),
            /* 386 */ new UdLookupTableListEntry( ud_itab__386, UdTableType.UD_TAB__OPC_VEX_W, "/vexw" ),
            /* 387 */ new UdLookupTableListEntry( ud_itab__387, UdTableType.UD_TAB__OPC_VEX_L, "/vexl" ),
            /* 388 */ new UdLookupTableListEntry( ud_itab__388, UdTableType.UD_TAB__OPC_VEX_W, "/vexw" ),
            /* 389 */ new UdLookupTableListEntry( ud_itab__389, UdTableType.UD_TAB__OPC_VEX_L, "/vexl" ),
            /* 390 */ new UdLookupTableListEntry( ud_itab__390, UdTableType.UD_TAB__OPC_VEX_L, "/vexl" ),
            /* 391 */ new UdLookupTableListEntry( ud_itab__391, UdTableType.UD_TAB__OPC_VEX_W, "/vexw" ),
            /* 392 */ new UdLookupTableListEntry( ud_itab__392, UdTableType.UD_TAB__OPC_VEX_W, "/vexw" ),
            /* 393 */ new UdLookupTableListEntry( ud_itab__393, UdTableType.UD_TAB__OPC_VEX_W, "/vexw" ),
            /* 394 */ new UdLookupTableListEntry( ud_itab__394, UdTableType.UD_TAB__OPC_TABLE, "opctbl" ),
            /* 395 */ new UdLookupTableListEntry( ud_itab__395, UdTableType.UD_TAB__OPC_MOD, "/mod" ),
            /* 396 */ new UdLookupTableListEntry( ud_itab__396, UdTableType.UD_TAB__OPC_MOD, "/mod" ),
            /* 397 */ new UdLookupTableListEntry( ud_itab__397, UdTableType.UD_TAB__OPC_MOD, "/mod" ),
            /* 398 */ new UdLookupTableListEntry( ud_itab__398, UdTableType.UD_TAB__OPC_MOD, "/mod" ),
            /* 399 */ new UdLookupTableListEntry( ud_itab__399, UdTableType.UD_TAB__OPC_TABLE, "opctbl" ),
            /* 400 */ new UdLookupTableListEntry( ud_itab__400, UdTableType.UD_TAB__OPC_MOD, "/mod" ),
            /* 401 */ new UdLookupTableListEntry( ud_itab__401, UdTableType.UD_TAB__OPC_MOD, "/mod" ),
            /* 402 */ new UdLookupTableListEntry( ud_itab__402, UdTableType.UD_TAB__OPC_MOD, "/mod" ),
            /* 403 */ new UdLookupTableListEntry( ud_itab__403, UdTableType.UD_TAB__OPC_VEX, "/vex" ),
            /* 404 */ new UdLookupTableListEntry( ud_itab__404, UdTableType.UD_TAB__OPC_MODE, "/m" ),
            /* 405 */ new UdLookupTableListEntry( ud_itab__405, UdTableType.UD_TAB__OPC_REG, "/reg" ),
            /* 406 */ new UdLookupTableListEntry( ud_itab__406, UdTableType.UD_TAB__OPC_REG, "/reg" ),
            /* 407 */ new UdLookupTableListEntry( ud_itab__407, UdTableType.UD_TAB__OPC_MODE, "/m" ),
            /* 408 */ new UdLookupTableListEntry( ud_itab__408, UdTableType.UD_TAB__OPC_OSIZE, "/o" ),
            /* 409 */ new UdLookupTableListEntry( ud_itab__409, UdTableType.UD_TAB__OPC_REG, "/reg" ),
            /* 410 */ new UdLookupTableListEntry( ud_itab__410, UdTableType.UD_TAB__OPC_REG, "/reg" ),
            /* 411 */ new UdLookupTableListEntry( ud_itab__411, UdTableType.UD_TAB__OPC_REG, "/reg" ),
            /* 412 */ new UdLookupTableListEntry( ud_itab__412, UdTableType.UD_TAB__OPC_REG, "/reg" ),
            /* 413 */ new UdLookupTableListEntry( ud_itab__413, UdTableType.UD_TAB__OPC_MODE, "/m" ),
            /* 414 */ new UdLookupTableListEntry( ud_itab__414, UdTableType.UD_TAB__OPC_MODE, "/m" ),
            /* 415 */ new UdLookupTableListEntry( ud_itab__415, UdTableType.UD_TAB__OPC_MODE, "/m" ),
            /* 416 */ new UdLookupTableListEntry( ud_itab__416, UdTableType.UD_TAB__OPC_MOD, "/mod" ),
            /* 417 */ new UdLookupTableListEntry( ud_itab__417, UdTableType.UD_TAB__OPC_REG, "/reg" ),
            /* 418 */ new UdLookupTableListEntry( ud_itab__418, UdTableType.UD_TAB__OPC_X87, "/x87" ),
            /* 419 */ new UdLookupTableListEntry( ud_itab__419, UdTableType.UD_TAB__OPC_MOD, "/mod" ),
            /* 420 */ new UdLookupTableListEntry( ud_itab__420, UdTableType.UD_TAB__OPC_REG, "/reg" ),
            /* 421 */ new UdLookupTableListEntry( ud_itab__421, UdTableType.UD_TAB__OPC_X87, "/x87" ),
            /* 422 */ new UdLookupTableListEntry( ud_itab__422, UdTableType.UD_TAB__OPC_MOD, "/mod" ),
            /* 423 */ new UdLookupTableListEntry( ud_itab__423, UdTableType.UD_TAB__OPC_REG, "/reg" ),
            /* 424 */ new UdLookupTableListEntry( ud_itab__424, UdTableType.UD_TAB__OPC_X87, "/x87" ),
            /* 425 */ new UdLookupTableListEntry( ud_itab__425, UdTableType.UD_TAB__OPC_MOD, "/mod" ),
            /* 426 */ new UdLookupTableListEntry( ud_itab__426, UdTableType.UD_TAB__OPC_REG, "/reg" ),
            /* 427 */ new UdLookupTableListEntry( ud_itab__427, UdTableType.UD_TAB__OPC_X87, "/x87" ),
            /* 428 */ new UdLookupTableListEntry( ud_itab__428, UdTableType.UD_TAB__OPC_MOD, "/mod" ),
            /* 429 */ new UdLookupTableListEntry( ud_itab__429, UdTableType.UD_TAB__OPC_REG, "/reg" ),
            /* 430 */ new UdLookupTableListEntry( ud_itab__430, UdTableType.UD_TAB__OPC_X87, "/x87" ),
            /* 431 */ new UdLookupTableListEntry( ud_itab__431, UdTableType.UD_TAB__OPC_MOD, "/mod" ),
            /* 432 */ new UdLookupTableListEntry( ud_itab__432, UdTableType.UD_TAB__OPC_REG, "/reg" ),
            /* 433 */ new UdLookupTableListEntry( ud_itab__433, UdTableType.UD_TAB__OPC_X87, "/x87" ),
            /* 434 */ new UdLookupTableListEntry( ud_itab__434, UdTableType.UD_TAB__OPC_MOD, "/mod" ),
            /* 435 */ new UdLookupTableListEntry( ud_itab__435, UdTableType.UD_TAB__OPC_REG, "/reg" ),
            /* 436 */ new UdLookupTableListEntry( ud_itab__436, UdTableType.UD_TAB__OPC_X87, "/x87" ),
            /* 437 */ new UdLookupTableListEntry( ud_itab__437, UdTableType.UD_TAB__OPC_MOD, "/mod" ),
            /* 438 */ new UdLookupTableListEntry( ud_itab__438, UdTableType.UD_TAB__OPC_REG, "/reg" ),
            /* 439 */ new UdLookupTableListEntry( ud_itab__439, UdTableType.UD_TAB__OPC_X87, "/x87" ),
            /* 440 */ new UdLookupTableListEntry( ud_itab__440, UdTableType.UD_TAB__OPC_ASIZE, "/a" ),
            /* 441 */ new UdLookupTableListEntry( ud_itab__441, UdTableType.UD_TAB__OPC_MODE, "/m" ),
            /* 442 */ new UdLookupTableListEntry( ud_itab__442, UdTableType.UD_TAB__OPC_REG, "/reg" ),
            /* 443 */ new UdLookupTableListEntry( ud_itab__443, UdTableType.UD_TAB__OPC_REG, "/reg" ),
            /* 444 */ new UdLookupTableListEntry( ud_itab__444, UdTableType.UD_TAB__OPC_REG, "/reg" ),
            /* 445 */ new UdLookupTableListEntry( ud_itab__445, UdTableType.UD_TAB__OPC_REG, "/reg" ),
            /* 446 */ new UdLookupTableListEntry( ud_itab__446, UdTableType.UD_TAB__OPC_MODE, "/m" ),
        };
        #endregion

        #region Operand Definitions

        /// <summary>
        /// itab entry operand definitions (for readability)
        /// </summary>
        internal static class OpDefs
        {
            internal static readonly UdItabEntryOperand O_AL = new UdItabEntryOperand(UdOperandCode.OP_AL, UdOperandSize.SZ_B);
            internal static readonly UdItabEntryOperand O_Av = new UdItabEntryOperand(UdOperandCode.OP_A, UdOperandSize.SZ_V);
            internal static readonly UdItabEntryOperand O_AX = new UdItabEntryOperand(UdOperandCode.OP_AX, UdOperandSize.SZ_W);
            internal static readonly UdItabEntryOperand O_C = new UdItabEntryOperand(UdOperandCode.OP_C, UdOperandSize.SZ_NA);
            internal static readonly UdItabEntryOperand O_CL = new UdItabEntryOperand(UdOperandCode.OP_CL, UdOperandSize.SZ_B);
            internal static readonly UdItabEntryOperand O_CS = new UdItabEntryOperand(UdOperandCode.OP_CS, UdOperandSize.SZ_NA);
            internal static readonly UdItabEntryOperand O_CX = new UdItabEntryOperand(UdOperandCode.OP_CX, UdOperandSize.SZ_W);
            internal static readonly UdItabEntryOperand O_D = new UdItabEntryOperand(UdOperandCode.OP_D, UdOperandSize.SZ_NA);
            internal static readonly UdItabEntryOperand O_DL = new UdItabEntryOperand(UdOperandCode.OP_DL, UdOperandSize.SZ_B);
            internal static readonly UdItabEntryOperand O_DS = new UdItabEntryOperand(UdOperandCode.OP_DS, UdOperandSize.SZ_NA);
            internal static readonly UdItabEntryOperand O_DX = new UdItabEntryOperand(UdOperandCode.OP_DX, UdOperandSize.SZ_W);
            internal static readonly UdItabEntryOperand O_E = new UdItabEntryOperand(UdOperandCode.OP_E, UdOperandSize.SZ_NA);
            internal static readonly UdItabEntryOperand O_eAX = new UdItabEntryOperand(UdOperandCode.OP_eAX, UdOperandSize.SZ_Z);
            internal static readonly UdItabEntryOperand O_Eb = new UdItabEntryOperand(UdOperandCode.OP_E, UdOperandSize.SZ_B);
            internal static readonly UdItabEntryOperand O_eCX = new UdItabEntryOperand(UdOperandCode.OP_eCX, UdOperandSize.SZ_Z);
            internal static readonly UdItabEntryOperand O_Ed = new UdItabEntryOperand(UdOperandCode.OP_E, UdOperandSize.SZ_D);
            internal static readonly UdItabEntryOperand O_eDX = new UdItabEntryOperand(UdOperandCode.OP_eDX, UdOperandSize.SZ_Z);
            internal static readonly UdItabEntryOperand O_Eq = new UdItabEntryOperand(UdOperandCode.OP_E, UdOperandSize.SZ_Q);
            internal static readonly UdItabEntryOperand O_ES = new UdItabEntryOperand(UdOperandCode.OP_ES, UdOperandSize.SZ_NA);
            internal static readonly UdItabEntryOperand O_Ev = new UdItabEntryOperand(UdOperandCode.OP_E, UdOperandSize.SZ_V);
            internal static readonly UdItabEntryOperand O_Ew = new UdItabEntryOperand(UdOperandCode.OP_E, UdOperandSize.SZ_W);
            internal static readonly UdItabEntryOperand O_Ey = new UdItabEntryOperand(UdOperandCode.OP_E, UdOperandSize.SZ_Y);
            internal static readonly UdItabEntryOperand O_Ez = new UdItabEntryOperand(UdOperandCode.OP_E, UdOperandSize.SZ_Z);
            internal static readonly UdItabEntryOperand O_FS = new UdItabEntryOperand(UdOperandCode.OP_FS, UdOperandSize.SZ_NA);
            internal static readonly UdItabEntryOperand O_Fv = new UdItabEntryOperand(UdOperandCode.OP_F, UdOperandSize.SZ_V);
            internal static readonly UdItabEntryOperand O_G = new UdItabEntryOperand(UdOperandCode.OP_G, UdOperandSize.SZ_NA);
            internal static readonly UdItabEntryOperand O_Gb = new UdItabEntryOperand(UdOperandCode.OP_G, UdOperandSize.SZ_B);
            internal static readonly UdItabEntryOperand O_Gd = new UdItabEntryOperand(UdOperandCode.OP_G, UdOperandSize.SZ_D);
            internal static readonly UdItabEntryOperand O_Gq = new UdItabEntryOperand(UdOperandCode.OP_G, UdOperandSize.SZ_Q);
            internal static readonly UdItabEntryOperand O_GS = new UdItabEntryOperand(UdOperandCode.OP_GS, UdOperandSize.SZ_NA);
            internal static readonly UdItabEntryOperand O_Gv = new UdItabEntryOperand(UdOperandCode.OP_G, UdOperandSize.SZ_V);
            internal static readonly UdItabEntryOperand O_Gw = new UdItabEntryOperand(UdOperandCode.OP_G, UdOperandSize.SZ_W);
            internal static readonly UdItabEntryOperand O_Gy = new UdItabEntryOperand(UdOperandCode.OP_G, UdOperandSize.SZ_Y);
            internal static readonly UdItabEntryOperand O_Gz = new UdItabEntryOperand(UdOperandCode.OP_G, UdOperandSize.SZ_Z);
            internal static readonly UdItabEntryOperand O_H = new UdItabEntryOperand(UdOperandCode.OP_H, UdOperandSize.SZ_X);
            internal static readonly UdItabEntryOperand O_Hqq = new UdItabEntryOperand(UdOperandCode.OP_H, UdOperandSize.SZ_QQ);
            internal static readonly UdItabEntryOperand O_Hx = new UdItabEntryOperand(UdOperandCode.OP_H, UdOperandSize.SZ_X);
            internal static readonly UdItabEntryOperand O_I1 = new UdItabEntryOperand(UdOperandCode.OP_I1, UdOperandSize.SZ_NA);
            internal static readonly UdItabEntryOperand O_I3 = new UdItabEntryOperand(UdOperandCode.OP_I3, UdOperandSize.SZ_NA);
            internal static readonly UdItabEntryOperand O_Ib = new UdItabEntryOperand(UdOperandCode.OP_I, UdOperandSize.SZ_B);
            internal static readonly UdItabEntryOperand O_Iv = new UdItabEntryOperand(UdOperandCode.OP_I, UdOperandSize.SZ_V);
            internal static readonly UdItabEntryOperand O_Iw = new UdItabEntryOperand(UdOperandCode.OP_I, UdOperandSize.SZ_W);
            internal static readonly UdItabEntryOperand O_Iz = new UdItabEntryOperand(UdOperandCode.OP_I, UdOperandSize.SZ_Z);
            internal static readonly UdItabEntryOperand O_Jb = new UdItabEntryOperand(UdOperandCode.OP_J, UdOperandSize.SZ_B);
            internal static readonly UdItabEntryOperand O_Jv = new UdItabEntryOperand(UdOperandCode.OP_J, UdOperandSize.SZ_V);
            internal static readonly UdItabEntryOperand O_Jz = new UdItabEntryOperand(UdOperandCode.OP_J, UdOperandSize.SZ_Z);
            internal static readonly UdItabEntryOperand O_L = new UdItabEntryOperand(UdOperandCode.OP_L, UdOperandSize.SZ_O);
            internal static readonly UdItabEntryOperand O_Lx = new UdItabEntryOperand(UdOperandCode.OP_L, UdOperandSize.SZ_X);
            internal static readonly UdItabEntryOperand O_M = new UdItabEntryOperand(UdOperandCode.OP_M, UdOperandSize.SZ_NA);
            internal static readonly UdItabEntryOperand O_Mb = new UdItabEntryOperand(UdOperandCode.OP_M, UdOperandSize.SZ_B);
            internal static readonly UdItabEntryOperand O_MbRd = new UdItabEntryOperand(UdOperandCode.OP_MR, UdOperandSize.SZ_BD);
            internal static readonly UdItabEntryOperand O_MbRv = new UdItabEntryOperand(UdOperandCode.OP_MR, UdOperandSize.SZ_BV);
            internal static readonly UdItabEntryOperand O_Md = new UdItabEntryOperand(UdOperandCode.OP_M, UdOperandSize.SZ_D);
            internal static readonly UdItabEntryOperand O_Mdq = new UdItabEntryOperand(UdOperandCode.OP_M, UdOperandSize.SZ_DQ);
            internal static readonly UdItabEntryOperand O_MdRy = new UdItabEntryOperand(UdOperandCode.OP_MR, UdOperandSize.SZ_DY);
            internal static readonly UdItabEntryOperand O_MdU = new UdItabEntryOperand(UdOperandCode.OP_MU, UdOperandSize.SZ_DO);
            internal static readonly UdItabEntryOperand O_Mo = new UdItabEntryOperand(UdOperandCode.OP_M, UdOperandSize.SZ_O);
            internal static readonly UdItabEntryOperand O_Mq = new UdItabEntryOperand(UdOperandCode.OP_M, UdOperandSize.SZ_Q);
            internal static readonly UdItabEntryOperand O_MqU = new UdItabEntryOperand(UdOperandCode.OP_MU, UdOperandSize.SZ_QO);
            internal static readonly UdItabEntryOperand O_Ms = new UdItabEntryOperand(UdOperandCode.OP_M, UdOperandSize.SZ_W);
            internal static readonly UdItabEntryOperand O_Mt = new UdItabEntryOperand(UdOperandCode.OP_M, UdOperandSize.SZ_T);
            internal static readonly UdItabEntryOperand O_Mv = new UdItabEntryOperand(UdOperandCode.OP_M, UdOperandSize.SZ_V);
            internal static readonly UdItabEntryOperand O_Mw = new UdItabEntryOperand(UdOperandCode.OP_M, UdOperandSize.SZ_W);
            internal static readonly UdItabEntryOperand O_MwRd = new UdItabEntryOperand(UdOperandCode.OP_MR, UdOperandSize.SZ_WD);
            internal static readonly UdItabEntryOperand O_MwRv = new UdItabEntryOperand(UdOperandCode.OP_MR, UdOperandSize.SZ_WV);
            internal static readonly UdItabEntryOperand O_MwRy = new UdItabEntryOperand(UdOperandCode.OP_MR, UdOperandSize.SZ_WY);
            internal static readonly UdItabEntryOperand O_MwU = new UdItabEntryOperand(UdOperandCode.OP_MU, UdOperandSize.SZ_WO);
            internal static readonly UdItabEntryOperand O_N = new UdItabEntryOperand(UdOperandCode.OP_N, UdOperandSize.SZ_Q);
            internal static readonly UdItabEntryOperand O_NONE = new UdItabEntryOperand(UdOperandCode.OP_NONE, UdOperandSize.SZ_NA);
            internal static readonly UdItabEntryOperand O_Ob = new UdItabEntryOperand(UdOperandCode.OP_O, UdOperandSize.SZ_B);
            internal static readonly UdItabEntryOperand O_Ov = new UdItabEntryOperand(UdOperandCode.OP_O, UdOperandSize.SZ_V);
            internal static readonly UdItabEntryOperand O_Ow = new UdItabEntryOperand(UdOperandCode.OP_O, UdOperandSize.SZ_W);
            internal static readonly UdItabEntryOperand O_P = new UdItabEntryOperand(UdOperandCode.OP_P, UdOperandSize.SZ_Q);
            internal static readonly UdItabEntryOperand O_Q = new UdItabEntryOperand(UdOperandCode.OP_Q, UdOperandSize.SZ_Q);
            internal static readonly UdItabEntryOperand O_R = new UdItabEntryOperand(UdOperandCode.OP_R, UdOperandSize.SZ_RDQ);
            internal static readonly UdItabEntryOperand O_R0b = new UdItabEntryOperand(UdOperandCode.OP_R0, UdOperandSize.SZ_B);
            internal static readonly UdItabEntryOperand O_R0v = new UdItabEntryOperand(UdOperandCode.OP_R0, UdOperandSize.SZ_V);
            internal static readonly UdItabEntryOperand O_R0w = new UdItabEntryOperand(UdOperandCode.OP_R0, UdOperandSize.SZ_W);
            internal static readonly UdItabEntryOperand O_R0y = new UdItabEntryOperand(UdOperandCode.OP_R0, UdOperandSize.SZ_Y);
            internal static readonly UdItabEntryOperand O_R0z = new UdItabEntryOperand(UdOperandCode.OP_R0, UdOperandSize.SZ_Z);
            internal static readonly UdItabEntryOperand O_R1b = new UdItabEntryOperand(UdOperandCode.OP_R1, UdOperandSize.SZ_B);
            internal static readonly UdItabEntryOperand O_R1v = new UdItabEntryOperand(UdOperandCode.OP_R1, UdOperandSize.SZ_V);
            internal static readonly UdItabEntryOperand O_R1w = new UdItabEntryOperand(UdOperandCode.OP_R1, UdOperandSize.SZ_W);
            internal static readonly UdItabEntryOperand O_R1y = new UdItabEntryOperand(UdOperandCode.OP_R1, UdOperandSize.SZ_Y);
            internal static readonly UdItabEntryOperand O_R1z = new UdItabEntryOperand(UdOperandCode.OP_R1, UdOperandSize.SZ_Z);
            internal static readonly UdItabEntryOperand O_R2b = new UdItabEntryOperand(UdOperandCode.OP_R2, UdOperandSize.SZ_B);
            internal static readonly UdItabEntryOperand O_R2v = new UdItabEntryOperand(UdOperandCode.OP_R2, UdOperandSize.SZ_V);
            internal static readonly UdItabEntryOperand O_R2w = new UdItabEntryOperand(UdOperandCode.OP_R2, UdOperandSize.SZ_W);
            internal static readonly UdItabEntryOperand O_R2y = new UdItabEntryOperand(UdOperandCode.OP_R2, UdOperandSize.SZ_Y);
            internal static readonly UdItabEntryOperand O_R2z = new UdItabEntryOperand(UdOperandCode.OP_R2, UdOperandSize.SZ_Z);
            internal static readonly UdItabEntryOperand O_R3b = new UdItabEntryOperand(UdOperandCode.OP_R3, UdOperandSize.SZ_B);
            internal static readonly UdItabEntryOperand O_R3v = new UdItabEntryOperand(UdOperandCode.OP_R3, UdOperandSize.SZ_V);
            internal static readonly UdItabEntryOperand O_R3w = new UdItabEntryOperand(UdOperandCode.OP_R3, UdOperandSize.SZ_W);
            internal static readonly UdItabEntryOperand O_R3y = new UdItabEntryOperand(UdOperandCode.OP_R3, UdOperandSize.SZ_Y);
            internal static readonly UdItabEntryOperand O_R3z = new UdItabEntryOperand(UdOperandCode.OP_R3, UdOperandSize.SZ_Z);
            internal static readonly UdItabEntryOperand O_R4b = new UdItabEntryOperand(UdOperandCode.OP_R4, UdOperandSize.SZ_B);
            internal static readonly UdItabEntryOperand O_R4v = new UdItabEntryOperand(UdOperandCode.OP_R4, UdOperandSize.SZ_V);
            internal static readonly UdItabEntryOperand O_R4w = new UdItabEntryOperand(UdOperandCode.OP_R4, UdOperandSize.SZ_W);
            internal static readonly UdItabEntryOperand O_R4y = new UdItabEntryOperand(UdOperandCode.OP_R4, UdOperandSize.SZ_Y);
            internal static readonly UdItabEntryOperand O_R4z = new UdItabEntryOperand(UdOperandCode.OP_R4, UdOperandSize.SZ_Z);
            internal static readonly UdItabEntryOperand O_R5b = new UdItabEntryOperand(UdOperandCode.OP_R5, UdOperandSize.SZ_B);
            internal static readonly UdItabEntryOperand O_R5v = new UdItabEntryOperand(UdOperandCode.OP_R5, UdOperandSize.SZ_V);
            internal static readonly UdItabEntryOperand O_R5w = new UdItabEntryOperand(UdOperandCode.OP_R5, UdOperandSize.SZ_W);
            internal static readonly UdItabEntryOperand O_R5y = new UdItabEntryOperand(UdOperandCode.OP_R5, UdOperandSize.SZ_Y);
            internal static readonly UdItabEntryOperand O_R5z = new UdItabEntryOperand(UdOperandCode.OP_R5, UdOperandSize.SZ_Z);
            internal static readonly UdItabEntryOperand O_R6b = new UdItabEntryOperand(UdOperandCode.OP_R6, UdOperandSize.SZ_B);
            internal static readonly UdItabEntryOperand O_R6v = new UdItabEntryOperand(UdOperandCode.OP_R6, UdOperandSize.SZ_V);
            internal static readonly UdItabEntryOperand O_R6w = new UdItabEntryOperand(UdOperandCode.OP_R6, UdOperandSize.SZ_W);
            internal static readonly UdItabEntryOperand O_R6y = new UdItabEntryOperand(UdOperandCode.OP_R6, UdOperandSize.SZ_Y);
            internal static readonly UdItabEntryOperand O_R6z = new UdItabEntryOperand(UdOperandCode.OP_R6, UdOperandSize.SZ_Z);
            internal static readonly UdItabEntryOperand O_R7b = new UdItabEntryOperand(UdOperandCode.OP_R7, UdOperandSize.SZ_B);
            internal static readonly UdItabEntryOperand O_R7v = new UdItabEntryOperand(UdOperandCode.OP_R7, UdOperandSize.SZ_V);
            internal static readonly UdItabEntryOperand O_R7w = new UdItabEntryOperand(UdOperandCode.OP_R7, UdOperandSize.SZ_W);
            internal static readonly UdItabEntryOperand O_R7y = new UdItabEntryOperand(UdOperandCode.OP_R7, UdOperandSize.SZ_Y);
            internal static readonly UdItabEntryOperand O_R7z = new UdItabEntryOperand(UdOperandCode.OP_R7, UdOperandSize.SZ_Z);
            internal static readonly UdItabEntryOperand O_rAX = new UdItabEntryOperand(UdOperandCode.OP_rAX, UdOperandSize.SZ_V);
            internal static readonly UdItabEntryOperand O_rCX = new UdItabEntryOperand(UdOperandCode.OP_rCX, UdOperandSize.SZ_V);
            internal static readonly UdItabEntryOperand O_rDX = new UdItabEntryOperand(UdOperandCode.OP_rDX, UdOperandSize.SZ_V);
            internal static readonly UdItabEntryOperand O_S = new UdItabEntryOperand(UdOperandCode.OP_S, UdOperandSize.SZ_W);
            internal static readonly UdItabEntryOperand O_sIb = new UdItabEntryOperand(UdOperandCode.OP_sI, UdOperandSize.SZ_B);
            internal static readonly UdItabEntryOperand O_sIv = new UdItabEntryOperand(UdOperandCode.OP_sI, UdOperandSize.SZ_V);
            internal static readonly UdItabEntryOperand O_sIz = new UdItabEntryOperand(UdOperandCode.OP_sI, UdOperandSize.SZ_Z);
            internal static readonly UdItabEntryOperand O_SS = new UdItabEntryOperand(UdOperandCode.OP_SS, UdOperandSize.SZ_NA);
            internal static readonly UdItabEntryOperand O_ST0 = new UdItabEntryOperand(UdOperandCode.OP_ST0, UdOperandSize.SZ_NA);
            internal static readonly UdItabEntryOperand O_ST1 = new UdItabEntryOperand(UdOperandCode.OP_ST1, UdOperandSize.SZ_NA);
            internal static readonly UdItabEntryOperand O_ST2 = new UdItabEntryOperand(UdOperandCode.OP_ST2, UdOperandSize.SZ_NA);
            internal static readonly UdItabEntryOperand O_ST3 = new UdItabEntryOperand(UdOperandCode.OP_ST3, UdOperandSize.SZ_NA);
            internal static readonly UdItabEntryOperand O_ST4 = new UdItabEntryOperand(UdOperandCode.OP_ST4, UdOperandSize.SZ_NA);
            internal static readonly UdItabEntryOperand O_ST5 = new UdItabEntryOperand(UdOperandCode.OP_ST5, UdOperandSize.SZ_NA);
            internal static readonly UdItabEntryOperand O_ST6 = new UdItabEntryOperand(UdOperandCode.OP_ST6, UdOperandSize.SZ_NA);
            internal static readonly UdItabEntryOperand O_ST7 = new UdItabEntryOperand(UdOperandCode.OP_ST7, UdOperandSize.SZ_NA);
            internal static readonly UdItabEntryOperand O_U = new UdItabEntryOperand(UdOperandCode.OP_U, UdOperandSize.SZ_O);
            internal static readonly UdItabEntryOperand O_Ux = new UdItabEntryOperand(UdOperandCode.OP_U, UdOperandSize.SZ_X);
            internal static readonly UdItabEntryOperand O_V = new UdItabEntryOperand(UdOperandCode.OP_V, UdOperandSize.SZ_DQ);
            internal static readonly UdItabEntryOperand O_Vdq = new UdItabEntryOperand(UdOperandCode.OP_V, UdOperandSize.SZ_DQ);
            internal static readonly UdItabEntryOperand O_Vqq = new UdItabEntryOperand(UdOperandCode.OP_V, UdOperandSize.SZ_QQ);
            internal static readonly UdItabEntryOperand O_Vsd = new UdItabEntryOperand(UdOperandCode.OP_V, UdOperandSize.SZ_Q);
            internal static readonly UdItabEntryOperand O_Vx = new UdItabEntryOperand(UdOperandCode.OP_V, UdOperandSize.SZ_X);
            internal static readonly UdItabEntryOperand O_W = new UdItabEntryOperand(UdOperandCode.OP_W, UdOperandSize.SZ_DQ);
            internal static readonly UdItabEntryOperand O_Wdq = new UdItabEntryOperand(UdOperandCode.OP_W, UdOperandSize.SZ_DQ);
            internal static readonly UdItabEntryOperand O_Wqq = new UdItabEntryOperand(UdOperandCode.OP_W, UdOperandSize.SZ_QQ);
            internal static readonly UdItabEntryOperand O_Wsd = new UdItabEntryOperand(UdOperandCode.OP_W, UdOperandSize.SZ_Q);
            internal static readonly UdItabEntryOperand O_Wx = new UdItabEntryOperand(UdOperandCode.OP_W, UdOperandSize.SZ_X);
        }
        #endregion

        #region Instruction Table and Mnemonics
        internal static readonly UdItabEntry[] ud_itab = new UdItabEntry[]
        {
            /* 0000 */ new UdItabEntry( UdMnemonicCode.UD_Iinvalid, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0001 */ new UdItabEntry( UdMnemonicCode.UD_Iaaa, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0002 */ new UdItabEntry( UdMnemonicCode.UD_Iaad, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0003 */ new UdItabEntry( UdMnemonicCode.UD_Iaam, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0004 */ new UdItabEntry( UdMnemonicCode.UD_Iaas, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0005 */ new UdItabEntry( UdMnemonicCode.UD_Iadc, OpDefs.O_Eb, OpDefs.O_Gb, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0006 */ new UdItabEntry( UdMnemonicCode.UD_Iadc, OpDefs.O_Ev, OpDefs.O_Gv, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0007 */ new UdItabEntry( UdMnemonicCode.UD_Iadc, OpDefs.O_Gb, OpDefs.O_Eb, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0008 */ new UdItabEntry( UdMnemonicCode.UD_Iadc, OpDefs.O_Gv, OpDefs.O_Ev, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0009 */ new UdItabEntry( UdMnemonicCode.UD_Iadc, OpDefs.O_AL, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0010 */ new UdItabEntry( UdMnemonicCode.UD_Iadc, OpDefs.O_rAX, OpDefs.O_sIz, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso|BitOps.PRexw ),
            /* 0011 */ new UdItabEntry( UdMnemonicCode.UD_Iadc, OpDefs.O_Eb, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0012 */ new UdItabEntry( UdMnemonicCode.UD_Iadc, OpDefs.O_Eb, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb|BitOps.PInv64 ),
            /* 0013 */ new UdItabEntry( UdMnemonicCode.UD_Iadc, OpDefs.O_Ev, OpDefs.O_sIz, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0014 */ new UdItabEntry( UdMnemonicCode.UD_Iadc, OpDefs.O_Ev, OpDefs.O_sIb, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0015 */ new UdItabEntry( UdMnemonicCode.UD_Iadd, OpDefs.O_Eb, OpDefs.O_Gb, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0016 */ new UdItabEntry( UdMnemonicCode.UD_Iadd, OpDefs.O_Ev, OpDefs.O_Gv, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0017 */ new UdItabEntry( UdMnemonicCode.UD_Iadd, OpDefs.O_Gb, OpDefs.O_Eb, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0018 */ new UdItabEntry( UdMnemonicCode.UD_Iadd, OpDefs.O_Gv, OpDefs.O_Ev, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0019 */ new UdItabEntry( UdMnemonicCode.UD_Iadd, OpDefs.O_AL, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0020 */ new UdItabEntry( UdMnemonicCode.UD_Iadd, OpDefs.O_rAX, OpDefs.O_sIz, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso|BitOps.PRexw ),
            /* 0021 */ new UdItabEntry( UdMnemonicCode.UD_Iadd, OpDefs.O_Eb, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0022 */ new UdItabEntry( UdMnemonicCode.UD_Iadd, OpDefs.O_Eb, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb|BitOps.PInv64 ),
            /* 0023 */ new UdItabEntry( UdMnemonicCode.UD_Iadd, OpDefs.O_Ev, OpDefs.O_sIz, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0024 */ new UdItabEntry( UdMnemonicCode.UD_Iadd, OpDefs.O_Ev, OpDefs.O_sIb, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0025 */ new UdItabEntry( UdMnemonicCode.UD_Iaddpd, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0026 */ new UdItabEntry( UdMnemonicCode.UD_Ivaddpd, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb|BitOps.PVexl ),
            /* 0027 */ new UdItabEntry( UdMnemonicCode.UD_Iaddps, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0028 */ new UdItabEntry( UdMnemonicCode.UD_Ivaddps, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb|BitOps.PVexl ),
            /* 0029 */ new UdItabEntry( UdMnemonicCode.UD_Iaddsd, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0030 */ new UdItabEntry( UdMnemonicCode.UD_Ivaddsd, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0031 */ new UdItabEntry( UdMnemonicCode.UD_Iaddss, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0032 */ new UdItabEntry( UdMnemonicCode.UD_Ivaddss, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0033 */ new UdItabEntry( UdMnemonicCode.UD_Iaddsubpd, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0034 */ new UdItabEntry( UdMnemonicCode.UD_Ivaddsubpd, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0035 */ new UdItabEntry( UdMnemonicCode.UD_Iaddsubps, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0036 */ new UdItabEntry( UdMnemonicCode.UD_Ivaddsubps, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0037 */ new UdItabEntry( UdMnemonicCode.UD_Iaesdec, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0038 */ new UdItabEntry( UdMnemonicCode.UD_Ivaesdec, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0039 */ new UdItabEntry( UdMnemonicCode.UD_Iaesdeclast, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0040 */ new UdItabEntry( UdMnemonicCode.UD_Ivaesdeclast, OpDefs.O_Vx, OpDefs.O_Wx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0041 */ new UdItabEntry( UdMnemonicCode.UD_Iaesenc, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0042 */ new UdItabEntry( UdMnemonicCode.UD_Ivaesenc, OpDefs.O_Vx, OpDefs.O_Wx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0043 */ new UdItabEntry( UdMnemonicCode.UD_Iaesenclast, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0044 */ new UdItabEntry( UdMnemonicCode.UD_Ivaesenclast, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0045 */ new UdItabEntry( UdMnemonicCode.UD_Iaesimc, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0046 */ new UdItabEntry( UdMnemonicCode.UD_Ivaesimc, OpDefs.O_Vx, OpDefs.O_Wx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0047 */ new UdItabEntry( UdMnemonicCode.UD_Iaeskeygenassist, OpDefs.O_V, OpDefs.O_W, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0048 */ new UdItabEntry( UdMnemonicCode.UD_Ivaeskeygenassist, OpDefs.O_Vx, OpDefs.O_Wx, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0049 */ new UdItabEntry( UdMnemonicCode.UD_Iand, OpDefs.O_Eb, OpDefs.O_Gb, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0050 */ new UdItabEntry( UdMnemonicCode.UD_Iand, OpDefs.O_Ev, OpDefs.O_Gv, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0051 */ new UdItabEntry( UdMnemonicCode.UD_Iand, OpDefs.O_Gb, OpDefs.O_Eb, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0052 */ new UdItabEntry( UdMnemonicCode.UD_Iand, OpDefs.O_Gv, OpDefs.O_Ev, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0053 */ new UdItabEntry( UdMnemonicCode.UD_Iand, OpDefs.O_AL, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0054 */ new UdItabEntry( UdMnemonicCode.UD_Iand, OpDefs.O_rAX, OpDefs.O_sIz, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso|BitOps.PRexw ),
            /* 0055 */ new UdItabEntry( UdMnemonicCode.UD_Iand, OpDefs.O_Eb, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0056 */ new UdItabEntry( UdMnemonicCode.UD_Iand, OpDefs.O_Eb, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb|BitOps.PInv64 ),
            /* 0057 */ new UdItabEntry( UdMnemonicCode.UD_Iand, OpDefs.O_Ev, OpDefs.O_sIz, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0058 */ new UdItabEntry( UdMnemonicCode.UD_Iand, OpDefs.O_Ev, OpDefs.O_sIb, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0059 */ new UdItabEntry( UdMnemonicCode.UD_Iandpd, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0060 */ new UdItabEntry( UdMnemonicCode.UD_Ivandpd, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb|BitOps.PVexl ),
            /* 0061 */ new UdItabEntry( UdMnemonicCode.UD_Iandps, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0062 */ new UdItabEntry( UdMnemonicCode.UD_Ivandps, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0063 */ new UdItabEntry( UdMnemonicCode.UD_Iandnpd, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0064 */ new UdItabEntry( UdMnemonicCode.UD_Ivandnpd, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0065 */ new UdItabEntry( UdMnemonicCode.UD_Iandnps, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0066 */ new UdItabEntry( UdMnemonicCode.UD_Ivandnps, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0067 */ new UdItabEntry( UdMnemonicCode.UD_Iarpl, OpDefs.O_Ew, OpDefs.O_Gw, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso ),
            /* 0068 */ new UdItabEntry( UdMnemonicCode.UD_Imovsxd, OpDefs.O_Gq, OpDefs.O_Ed, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexx|BitOps.PRexr|BitOps.PRexb ),
            /* 0069 */ new UdItabEntry( UdMnemonicCode.UD_Icall, OpDefs.O_Ev, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0070 */ new UdItabEntry( UdMnemonicCode.UD_Icall, OpDefs.O_Eq, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb|BitOps.PDef64 ),
            /* 0071 */ new UdItabEntry( UdMnemonicCode.UD_Icall, OpDefs.O_Fv, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0072 */ new UdItabEntry( UdMnemonicCode.UD_Icall, OpDefs.O_Jz, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso|BitOps.PDef64 ),
            /* 0073 */ new UdItabEntry( UdMnemonicCode.UD_Icall, OpDefs.O_Av, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso ),
            /* 0074 */ new UdItabEntry( UdMnemonicCode.UD_Icbw, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso|BitOps.PRexw ),
            /* 0075 */ new UdItabEntry( UdMnemonicCode.UD_Icwde, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso|BitOps.PRexw ),
            /* 0076 */ new UdItabEntry( UdMnemonicCode.UD_Icdqe, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso|BitOps.PRexw ),
            /* 0077 */ new UdItabEntry( UdMnemonicCode.UD_Iclc, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0078 */ new UdItabEntry( UdMnemonicCode.UD_Icld, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0079 */ new UdItabEntry( UdMnemonicCode.UD_Iclflush, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0080 */ new UdItabEntry( UdMnemonicCode.UD_Iclgi, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0081 */ new UdItabEntry( UdMnemonicCode.UD_Icli, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0082 */ new UdItabEntry( UdMnemonicCode.UD_Iclts, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0083 */ new UdItabEntry( UdMnemonicCode.UD_Icmc, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0084 */ new UdItabEntry( UdMnemonicCode.UD_Icmovo, OpDefs.O_Gv, OpDefs.O_Ev, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0085 */ new UdItabEntry( UdMnemonicCode.UD_Icmovno, OpDefs.O_Gv, OpDefs.O_Ev, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0086 */ new UdItabEntry( UdMnemonicCode.UD_Icmovb, OpDefs.O_Gv, OpDefs.O_Ev, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0087 */ new UdItabEntry( UdMnemonicCode.UD_Icmovae, OpDefs.O_Gv, OpDefs.O_Ev, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0088 */ new UdItabEntry( UdMnemonicCode.UD_Icmovz, OpDefs.O_Gv, OpDefs.O_Ev, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0089 */ new UdItabEntry( UdMnemonicCode.UD_Icmovnz, OpDefs.O_Gv, OpDefs.O_Ev, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0090 */ new UdItabEntry( UdMnemonicCode.UD_Icmovbe, OpDefs.O_Gv, OpDefs.O_Ev, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0091 */ new UdItabEntry( UdMnemonicCode.UD_Icmova, OpDefs.O_Gv, OpDefs.O_Ev, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0092 */ new UdItabEntry( UdMnemonicCode.UD_Icmovs, OpDefs.O_Gv, OpDefs.O_Ev, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0093 */ new UdItabEntry( UdMnemonicCode.UD_Icmovns, OpDefs.O_Gv, OpDefs.O_Ev, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0094 */ new UdItabEntry( UdMnemonicCode.UD_Icmovp, OpDefs.O_Gv, OpDefs.O_Ev, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0095 */ new UdItabEntry( UdMnemonicCode.UD_Icmovnp, OpDefs.O_Gv, OpDefs.O_Ev, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0096 */ new UdItabEntry( UdMnemonicCode.UD_Icmovl, OpDefs.O_Gv, OpDefs.O_Ev, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0097 */ new UdItabEntry( UdMnemonicCode.UD_Icmovge, OpDefs.O_Gv, OpDefs.O_Ev, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0098 */ new UdItabEntry( UdMnemonicCode.UD_Icmovle, OpDefs.O_Gv, OpDefs.O_Ev, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0099 */ new UdItabEntry( UdMnemonicCode.UD_Icmovg, OpDefs.O_Gv, OpDefs.O_Ev, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0100 */ new UdItabEntry( UdMnemonicCode.UD_Icmp, OpDefs.O_Eb, OpDefs.O_Gb, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0101 */ new UdItabEntry( UdMnemonicCode.UD_Icmp, OpDefs.O_Ev, OpDefs.O_Gv, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0102 */ new UdItabEntry( UdMnemonicCode.UD_Icmp, OpDefs.O_Gb, OpDefs.O_Eb, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0103 */ new UdItabEntry( UdMnemonicCode.UD_Icmp, OpDefs.O_Gv, OpDefs.O_Ev, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0104 */ new UdItabEntry( UdMnemonicCode.UD_Icmp, OpDefs.O_AL, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0105 */ new UdItabEntry( UdMnemonicCode.UD_Icmp, OpDefs.O_rAX, OpDefs.O_sIz, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso|BitOps.PRexw ),
            /* 0106 */ new UdItabEntry( UdMnemonicCode.UD_Icmp, OpDefs.O_Eb, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0107 */ new UdItabEntry( UdMnemonicCode.UD_Icmp, OpDefs.O_Eb, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb|BitOps.PInv64 ),
            /* 0108 */ new UdItabEntry( UdMnemonicCode.UD_Icmp, OpDefs.O_Ev, OpDefs.O_sIz, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0109 */ new UdItabEntry( UdMnemonicCode.UD_Icmp, OpDefs.O_Ev, OpDefs.O_sIb, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0110 */ new UdItabEntry( UdMnemonicCode.UD_Icmppd, OpDefs.O_V, OpDefs.O_W, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0111 */ new UdItabEntry( UdMnemonicCode.UD_Ivcmppd, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_Ib, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb|BitOps.PVexl ),
            /* 0112 */ new UdItabEntry( UdMnemonicCode.UD_Icmpps, OpDefs.O_V, OpDefs.O_W, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0113 */ new UdItabEntry( UdMnemonicCode.UD_Ivcmpps, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_Ib, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb|BitOps.PVexl ),
            /* 0114 */ new UdItabEntry( UdMnemonicCode.UD_Icmpsb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PStrz|BitOps.PSeg ),
            /* 0115 */ new UdItabEntry( UdMnemonicCode.UD_Icmpsw, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PStrz|BitOps.POso|BitOps.PRexw|BitOps.PSeg ),
            /* 0116 */ new UdItabEntry( UdMnemonicCode.UD_Icmpsd, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PStrz|BitOps.POso|BitOps.PRexw|BitOps.PSeg ),
            /* 0117 */ new UdItabEntry( UdMnemonicCode.UD_Icmpsd, OpDefs.O_V, OpDefs.O_W, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0118 */ new UdItabEntry( UdMnemonicCode.UD_Ivcmpsd, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_Ib, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0119 */ new UdItabEntry( UdMnemonicCode.UD_Icmpsq, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PStrz|BitOps.POso|BitOps.PRexw|BitOps.PSeg ),
            /* 0120 */ new UdItabEntry( UdMnemonicCode.UD_Icmpss, OpDefs.O_V, OpDefs.O_W, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0121 */ new UdItabEntry( UdMnemonicCode.UD_Ivcmpss, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_Ib, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0122 */ new UdItabEntry( UdMnemonicCode.UD_Icmpxchg, OpDefs.O_Eb, OpDefs.O_Gb, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0123 */ new UdItabEntry( UdMnemonicCode.UD_Icmpxchg, OpDefs.O_Ev, OpDefs.O_Gv, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0124 */ new UdItabEntry( UdMnemonicCode.UD_Icmpxchg8b, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0125 */ new UdItabEntry( UdMnemonicCode.UD_Icmpxchg8b, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0126 */ new UdItabEntry( UdMnemonicCode.UD_Icmpxchg16b, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0127 */ new UdItabEntry( UdMnemonicCode.UD_Icomisd, OpDefs.O_Vsd, OpDefs.O_Wsd, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0128 */ new UdItabEntry( UdMnemonicCode.UD_Ivcomisd, OpDefs.O_Vsd, OpDefs.O_Wsd, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0129 */ new UdItabEntry( UdMnemonicCode.UD_Icomiss, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0130 */ new UdItabEntry( UdMnemonicCode.UD_Ivcomiss, OpDefs.O_Vx, OpDefs.O_Wx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0131 */ new UdItabEntry( UdMnemonicCode.UD_Icpuid, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0132 */ new UdItabEntry( UdMnemonicCode.UD_Icvtdq2pd, OpDefs.O_V, OpDefs.O_Wdq, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0133 */ new UdItabEntry( UdMnemonicCode.UD_Ivcvtdq2pd, OpDefs.O_Vx, OpDefs.O_Wdq, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb|BitOps.PVexl ),
            /* 0134 */ new UdItabEntry( UdMnemonicCode.UD_Icvtdq2ps, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0135 */ new UdItabEntry( UdMnemonicCode.UD_Ivcvtdq2ps, OpDefs.O_Vx, OpDefs.O_Wx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0136 */ new UdItabEntry( UdMnemonicCode.UD_Icvtpd2dq, OpDefs.O_Vdq, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0137 */ new UdItabEntry( UdMnemonicCode.UD_Ivcvtpd2dq, OpDefs.O_Vdq, OpDefs.O_Wx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb|BitOps.PVexl ),
            /* 0138 */ new UdItabEntry( UdMnemonicCode.UD_Icvtpd2pi, OpDefs.O_P, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0139 */ new UdItabEntry( UdMnemonicCode.UD_Icvtpd2ps, OpDefs.O_Vdq, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0140 */ new UdItabEntry( UdMnemonicCode.UD_Ivcvtpd2ps, OpDefs.O_Vdq, OpDefs.O_Wx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb|BitOps.PVexl ),
            /* 0141 */ new UdItabEntry( UdMnemonicCode.UD_Icvtpi2ps, OpDefs.O_V, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0142 */ new UdItabEntry( UdMnemonicCode.UD_Icvtpi2pd, OpDefs.O_V, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0143 */ new UdItabEntry( UdMnemonicCode.UD_Icvtps2dq, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0144 */ new UdItabEntry( UdMnemonicCode.UD_Ivcvtps2dq, OpDefs.O_Vx, OpDefs.O_Wx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb|BitOps.PVexl ),
            /* 0145 */ new UdItabEntry( UdMnemonicCode.UD_Icvtps2pd, OpDefs.O_V, OpDefs.O_Wdq, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0146 */ new UdItabEntry( UdMnemonicCode.UD_Ivcvtps2pd, OpDefs.O_Vx, OpDefs.O_Wdq, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb|BitOps.PVexl ),
            /* 0147 */ new UdItabEntry( UdMnemonicCode.UD_Icvtps2pi, OpDefs.O_P, OpDefs.O_MqU, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0148 */ new UdItabEntry( UdMnemonicCode.UD_Icvtsd2si, OpDefs.O_Gy, OpDefs.O_MqU, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0149 */ new UdItabEntry( UdMnemonicCode.UD_Ivcvtsd2si, OpDefs.O_Gy, OpDefs.O_MqU, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0150 */ new UdItabEntry( UdMnemonicCode.UD_Icvtsd2ss, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0151 */ new UdItabEntry( UdMnemonicCode.UD_Ivcvtsd2ss, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0152 */ new UdItabEntry( UdMnemonicCode.UD_Icvtsi2sd, OpDefs.O_V, OpDefs.O_Ey, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0153 */ new UdItabEntry( UdMnemonicCode.UD_Ivcvtsi2sd, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Ey, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0154 */ new UdItabEntry( UdMnemonicCode.UD_Icvtsi2ss, OpDefs.O_V, OpDefs.O_Ey, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0155 */ new UdItabEntry( UdMnemonicCode.UD_Ivcvtsi2ss, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Ey, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0156 */ new UdItabEntry( UdMnemonicCode.UD_Icvtss2sd, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0157 */ new UdItabEntry( UdMnemonicCode.UD_Ivcvtss2sd, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0158 */ new UdItabEntry( UdMnemonicCode.UD_Icvtss2si, OpDefs.O_Gy, OpDefs.O_MdU, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0159 */ new UdItabEntry( UdMnemonicCode.UD_Ivcvtss2si, OpDefs.O_Gy, OpDefs.O_MdU, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0160 */ new UdItabEntry( UdMnemonicCode.UD_Icvttpd2dq, OpDefs.O_Vdq, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0161 */ new UdItabEntry( UdMnemonicCode.UD_Ivcvttpd2dq, OpDefs.O_Vdq, OpDefs.O_Wx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb|BitOps.PVexl ),
            /* 0162 */ new UdItabEntry( UdMnemonicCode.UD_Icvttpd2pi, OpDefs.O_P, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0163 */ new UdItabEntry( UdMnemonicCode.UD_Icvttps2dq, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0164 */ new UdItabEntry( UdMnemonicCode.UD_Ivcvttps2dq, OpDefs.O_Vx, OpDefs.O_Wx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb|BitOps.PVexl ),
            /* 0165 */ new UdItabEntry( UdMnemonicCode.UD_Icvttps2pi, OpDefs.O_P, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0166 */ new UdItabEntry( UdMnemonicCode.UD_Icvttsd2si, OpDefs.O_Gy, OpDefs.O_MqU, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0167 */ new UdItabEntry( UdMnemonicCode.UD_Ivcvttsd2si, OpDefs.O_Gy, OpDefs.O_MqU, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0168 */ new UdItabEntry( UdMnemonicCode.UD_Icvttss2si, OpDefs.O_Gy, OpDefs.O_MdU, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0169 */ new UdItabEntry( UdMnemonicCode.UD_Ivcvttss2si, OpDefs.O_Gy, OpDefs.O_MdU, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0170 */ new UdItabEntry( UdMnemonicCode.UD_Icwd, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso|BitOps.PRexw ),
            /* 0171 */ new UdItabEntry( UdMnemonicCode.UD_Icdq, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso|BitOps.PRexw ),
            /* 0172 */ new UdItabEntry( UdMnemonicCode.UD_Icqo, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso|BitOps.PRexw ),
            /* 0173 */ new UdItabEntry( UdMnemonicCode.UD_Idaa, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PInv64 ),
            /* 0174 */ new UdItabEntry( UdMnemonicCode.UD_Idas, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PInv64 ),
            /* 0175 */ new UdItabEntry( UdMnemonicCode.UD_Idec, OpDefs.O_R0z, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso ),
            /* 0176 */ new UdItabEntry( UdMnemonicCode.UD_Idec, OpDefs.O_R1z, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso ),
            /* 0177 */ new UdItabEntry( UdMnemonicCode.UD_Idec, OpDefs.O_R2z, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso ),
            /* 0178 */ new UdItabEntry( UdMnemonicCode.UD_Idec, OpDefs.O_R3z, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso ),
            /* 0179 */ new UdItabEntry( UdMnemonicCode.UD_Idec, OpDefs.O_R4z, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso ),
            /* 0180 */ new UdItabEntry( UdMnemonicCode.UD_Idec, OpDefs.O_R5z, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso ),
            /* 0181 */ new UdItabEntry( UdMnemonicCode.UD_Idec, OpDefs.O_R6z, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso ),
            /* 0182 */ new UdItabEntry( UdMnemonicCode.UD_Idec, OpDefs.O_R7z, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso ),
            /* 0183 */ new UdItabEntry( UdMnemonicCode.UD_Idec, OpDefs.O_Eb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0184 */ new UdItabEntry( UdMnemonicCode.UD_Idec, OpDefs.O_Ev, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0185 */ new UdItabEntry( UdMnemonicCode.UD_Idiv, OpDefs.O_Ev, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0186 */ new UdItabEntry( UdMnemonicCode.UD_Idiv, OpDefs.O_Eb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0187 */ new UdItabEntry( UdMnemonicCode.UD_Idivpd, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0188 */ new UdItabEntry( UdMnemonicCode.UD_Ivdivpd, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb|BitOps.PVexl ),
            /* 0189 */ new UdItabEntry( UdMnemonicCode.UD_Idivps, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0190 */ new UdItabEntry( UdMnemonicCode.UD_Ivdivps, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0191 */ new UdItabEntry( UdMnemonicCode.UD_Idivsd, OpDefs.O_V, OpDefs.O_MqU, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0192 */ new UdItabEntry( UdMnemonicCode.UD_Ivdivsd, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_MqU, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0193 */ new UdItabEntry( UdMnemonicCode.UD_Idivss, OpDefs.O_V, OpDefs.O_MdU, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0194 */ new UdItabEntry( UdMnemonicCode.UD_Ivdivss, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_MdU, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0195 */ new UdItabEntry( UdMnemonicCode.UD_Idppd, OpDefs.O_V, OpDefs.O_W, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0196 */ new UdItabEntry( UdMnemonicCode.UD_Ivdppd, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_Ib, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0197 */ new UdItabEntry( UdMnemonicCode.UD_Idpps, OpDefs.O_V, OpDefs.O_W, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0198 */ new UdItabEntry( UdMnemonicCode.UD_Ivdpps, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_Ib, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb|BitOps.PVexl ),
            /* 0199 */ new UdItabEntry( UdMnemonicCode.UD_Iemms, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0200 */ new UdItabEntry( UdMnemonicCode.UD_Ienter, OpDefs.O_Iw, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PDef64 ),
            /* 0201 */ new UdItabEntry( UdMnemonicCode.UD_Iextractps, OpDefs.O_MdRy, OpDefs.O_V, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexw|BitOps.PRexx|BitOps.PRexb ),
            /* 0202 */ new UdItabEntry( UdMnemonicCode.UD_Ivextractps, OpDefs.O_MdRy, OpDefs.O_Vx, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexw|BitOps.PRexx|BitOps.PRexb ),
            /* 0203 */ new UdItabEntry( UdMnemonicCode.UD_If2xm1, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0204 */ new UdItabEntry( UdMnemonicCode.UD_Ifabs, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0205 */ new UdItabEntry( UdMnemonicCode.UD_Ifadd, OpDefs.O_Mq, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0206 */ new UdItabEntry( UdMnemonicCode.UD_Ifadd, OpDefs.O_Md, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0207 */ new UdItabEntry( UdMnemonicCode.UD_Ifadd, OpDefs.O_ST0, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0208 */ new UdItabEntry( UdMnemonicCode.UD_Ifadd, OpDefs.O_ST1, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0209 */ new UdItabEntry( UdMnemonicCode.UD_Ifadd, OpDefs.O_ST2, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0210 */ new UdItabEntry( UdMnemonicCode.UD_Ifadd, OpDefs.O_ST3, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0211 */ new UdItabEntry( UdMnemonicCode.UD_Ifadd, OpDefs.O_ST4, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0212 */ new UdItabEntry( UdMnemonicCode.UD_Ifadd, OpDefs.O_ST5, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0213 */ new UdItabEntry( UdMnemonicCode.UD_Ifadd, OpDefs.O_ST6, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0214 */ new UdItabEntry( UdMnemonicCode.UD_Ifadd, OpDefs.O_ST7, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0215 */ new UdItabEntry( UdMnemonicCode.UD_Ifadd, OpDefs.O_ST0, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0216 */ new UdItabEntry( UdMnemonicCode.UD_Ifadd, OpDefs.O_ST0, OpDefs.O_ST1, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0217 */ new UdItabEntry( UdMnemonicCode.UD_Ifadd, OpDefs.O_ST0, OpDefs.O_ST2, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0218 */ new UdItabEntry( UdMnemonicCode.UD_Ifadd, OpDefs.O_ST0, OpDefs.O_ST3, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0219 */ new UdItabEntry( UdMnemonicCode.UD_Ifadd, OpDefs.O_ST0, OpDefs.O_ST4, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0220 */ new UdItabEntry( UdMnemonicCode.UD_Ifadd, OpDefs.O_ST0, OpDefs.O_ST5, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0221 */ new UdItabEntry( UdMnemonicCode.UD_Ifadd, OpDefs.O_ST0, OpDefs.O_ST6, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0222 */ new UdItabEntry( UdMnemonicCode.UD_Ifadd, OpDefs.O_ST0, OpDefs.O_ST7, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0223 */ new UdItabEntry( UdMnemonicCode.UD_Ifaddp, OpDefs.O_ST0, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0224 */ new UdItabEntry( UdMnemonicCode.UD_Ifaddp, OpDefs.O_ST1, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0225 */ new UdItabEntry( UdMnemonicCode.UD_Ifaddp, OpDefs.O_ST2, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0226 */ new UdItabEntry( UdMnemonicCode.UD_Ifaddp, OpDefs.O_ST3, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0227 */ new UdItabEntry( UdMnemonicCode.UD_Ifaddp, OpDefs.O_ST4, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0228 */ new UdItabEntry( UdMnemonicCode.UD_Ifaddp, OpDefs.O_ST5, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0229 */ new UdItabEntry( UdMnemonicCode.UD_Ifaddp, OpDefs.O_ST6, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0230 */ new UdItabEntry( UdMnemonicCode.UD_Ifaddp, OpDefs.O_ST7, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0231 */ new UdItabEntry( UdMnemonicCode.UD_Ifbld, OpDefs.O_Mt, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0232 */ new UdItabEntry( UdMnemonicCode.UD_Ifbstp, OpDefs.O_Mt, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0233 */ new UdItabEntry( UdMnemonicCode.UD_Ifchs, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0234 */ new UdItabEntry( UdMnemonicCode.UD_Ifclex, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0235 */ new UdItabEntry( UdMnemonicCode.UD_Ifcmovb, OpDefs.O_ST0, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0236 */ new UdItabEntry( UdMnemonicCode.UD_Ifcmovb, OpDefs.O_ST0, OpDefs.O_ST1, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0237 */ new UdItabEntry( UdMnemonicCode.UD_Ifcmovb, OpDefs.O_ST0, OpDefs.O_ST2, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0238 */ new UdItabEntry( UdMnemonicCode.UD_Ifcmovb, OpDefs.O_ST0, OpDefs.O_ST3, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0239 */ new UdItabEntry( UdMnemonicCode.UD_Ifcmovb, OpDefs.O_ST0, OpDefs.O_ST4, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0240 */ new UdItabEntry( UdMnemonicCode.UD_Ifcmovb, OpDefs.O_ST0, OpDefs.O_ST5, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0241 */ new UdItabEntry( UdMnemonicCode.UD_Ifcmovb, OpDefs.O_ST0, OpDefs.O_ST6, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0242 */ new UdItabEntry( UdMnemonicCode.UD_Ifcmovb, OpDefs.O_ST0, OpDefs.O_ST7, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0243 */ new UdItabEntry( UdMnemonicCode.UD_Ifcmove, OpDefs.O_ST0, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0244 */ new UdItabEntry( UdMnemonicCode.UD_Ifcmove, OpDefs.O_ST0, OpDefs.O_ST1, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0245 */ new UdItabEntry( UdMnemonicCode.UD_Ifcmove, OpDefs.O_ST0, OpDefs.O_ST2, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0246 */ new UdItabEntry( UdMnemonicCode.UD_Ifcmove, OpDefs.O_ST0, OpDefs.O_ST3, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0247 */ new UdItabEntry( UdMnemonicCode.UD_Ifcmove, OpDefs.O_ST0, OpDefs.O_ST4, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0248 */ new UdItabEntry( UdMnemonicCode.UD_Ifcmove, OpDefs.O_ST0, OpDefs.O_ST5, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0249 */ new UdItabEntry( UdMnemonicCode.UD_Ifcmove, OpDefs.O_ST0, OpDefs.O_ST6, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0250 */ new UdItabEntry( UdMnemonicCode.UD_Ifcmove, OpDefs.O_ST0, OpDefs.O_ST7, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0251 */ new UdItabEntry( UdMnemonicCode.UD_Ifcmovbe, OpDefs.O_ST0, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0252 */ new UdItabEntry( UdMnemonicCode.UD_Ifcmovbe, OpDefs.O_ST0, OpDefs.O_ST1, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0253 */ new UdItabEntry( UdMnemonicCode.UD_Ifcmovbe, OpDefs.O_ST0, OpDefs.O_ST2, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0254 */ new UdItabEntry( UdMnemonicCode.UD_Ifcmovbe, OpDefs.O_ST0, OpDefs.O_ST3, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0255 */ new UdItabEntry( UdMnemonicCode.UD_Ifcmovbe, OpDefs.O_ST0, OpDefs.O_ST4, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0256 */ new UdItabEntry( UdMnemonicCode.UD_Ifcmovbe, OpDefs.O_ST0, OpDefs.O_ST5, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0257 */ new UdItabEntry( UdMnemonicCode.UD_Ifcmovbe, OpDefs.O_ST0, OpDefs.O_ST6, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0258 */ new UdItabEntry( UdMnemonicCode.UD_Ifcmovbe, OpDefs.O_ST0, OpDefs.O_ST7, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0259 */ new UdItabEntry( UdMnemonicCode.UD_Ifcmovu, OpDefs.O_ST0, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0260 */ new UdItabEntry( UdMnemonicCode.UD_Ifcmovu, OpDefs.O_ST0, OpDefs.O_ST1, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0261 */ new UdItabEntry( UdMnemonicCode.UD_Ifcmovu, OpDefs.O_ST0, OpDefs.O_ST2, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0262 */ new UdItabEntry( UdMnemonicCode.UD_Ifcmovu, OpDefs.O_ST0, OpDefs.O_ST3, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0263 */ new UdItabEntry( UdMnemonicCode.UD_Ifcmovu, OpDefs.O_ST0, OpDefs.O_ST4, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0264 */ new UdItabEntry( UdMnemonicCode.UD_Ifcmovu, OpDefs.O_ST0, OpDefs.O_ST5, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0265 */ new UdItabEntry( UdMnemonicCode.UD_Ifcmovu, OpDefs.O_ST0, OpDefs.O_ST6, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0266 */ new UdItabEntry( UdMnemonicCode.UD_Ifcmovu, OpDefs.O_ST0, OpDefs.O_ST7, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0267 */ new UdItabEntry( UdMnemonicCode.UD_Ifcmovnb, OpDefs.O_ST0, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0268 */ new UdItabEntry( UdMnemonicCode.UD_Ifcmovnb, OpDefs.O_ST0, OpDefs.O_ST1, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0269 */ new UdItabEntry( UdMnemonicCode.UD_Ifcmovnb, OpDefs.O_ST0, OpDefs.O_ST2, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0270 */ new UdItabEntry( UdMnemonicCode.UD_Ifcmovnb, OpDefs.O_ST0, OpDefs.O_ST3, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0271 */ new UdItabEntry( UdMnemonicCode.UD_Ifcmovnb, OpDefs.O_ST0, OpDefs.O_ST4, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0272 */ new UdItabEntry( UdMnemonicCode.UD_Ifcmovnb, OpDefs.O_ST0, OpDefs.O_ST5, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0273 */ new UdItabEntry( UdMnemonicCode.UD_Ifcmovnb, OpDefs.O_ST0, OpDefs.O_ST6, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0274 */ new UdItabEntry( UdMnemonicCode.UD_Ifcmovnb, OpDefs.O_ST0, OpDefs.O_ST7, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0275 */ new UdItabEntry( UdMnemonicCode.UD_Ifcmovne, OpDefs.O_ST0, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0276 */ new UdItabEntry( UdMnemonicCode.UD_Ifcmovne, OpDefs.O_ST0, OpDefs.O_ST1, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0277 */ new UdItabEntry( UdMnemonicCode.UD_Ifcmovne, OpDefs.O_ST0, OpDefs.O_ST2, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0278 */ new UdItabEntry( UdMnemonicCode.UD_Ifcmovne, OpDefs.O_ST0, OpDefs.O_ST3, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0279 */ new UdItabEntry( UdMnemonicCode.UD_Ifcmovne, OpDefs.O_ST0, OpDefs.O_ST4, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0280 */ new UdItabEntry( UdMnemonicCode.UD_Ifcmovne, OpDefs.O_ST0, OpDefs.O_ST5, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0281 */ new UdItabEntry( UdMnemonicCode.UD_Ifcmovne, OpDefs.O_ST0, OpDefs.O_ST6, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0282 */ new UdItabEntry( UdMnemonicCode.UD_Ifcmovne, OpDefs.O_ST0, OpDefs.O_ST7, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0283 */ new UdItabEntry( UdMnemonicCode.UD_Ifcmovnbe, OpDefs.O_ST0, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0284 */ new UdItabEntry( UdMnemonicCode.UD_Ifcmovnbe, OpDefs.O_ST0, OpDefs.O_ST1, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0285 */ new UdItabEntry( UdMnemonicCode.UD_Ifcmovnbe, OpDefs.O_ST0, OpDefs.O_ST2, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0286 */ new UdItabEntry( UdMnemonicCode.UD_Ifcmovnbe, OpDefs.O_ST0, OpDefs.O_ST3, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0287 */ new UdItabEntry( UdMnemonicCode.UD_Ifcmovnbe, OpDefs.O_ST0, OpDefs.O_ST4, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0288 */ new UdItabEntry( UdMnemonicCode.UD_Ifcmovnbe, OpDefs.O_ST0, OpDefs.O_ST5, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0289 */ new UdItabEntry( UdMnemonicCode.UD_Ifcmovnbe, OpDefs.O_ST0, OpDefs.O_ST6, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0290 */ new UdItabEntry( UdMnemonicCode.UD_Ifcmovnbe, OpDefs.O_ST0, OpDefs.O_ST7, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0291 */ new UdItabEntry( UdMnemonicCode.UD_Ifcmovnu, OpDefs.O_ST0, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0292 */ new UdItabEntry( UdMnemonicCode.UD_Ifcmovnu, OpDefs.O_ST0, OpDefs.O_ST1, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0293 */ new UdItabEntry( UdMnemonicCode.UD_Ifcmovnu, OpDefs.O_ST0, OpDefs.O_ST2, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0294 */ new UdItabEntry( UdMnemonicCode.UD_Ifcmovnu, OpDefs.O_ST0, OpDefs.O_ST3, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0295 */ new UdItabEntry( UdMnemonicCode.UD_Ifcmovnu, OpDefs.O_ST0, OpDefs.O_ST4, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0296 */ new UdItabEntry( UdMnemonicCode.UD_Ifcmovnu, OpDefs.O_ST0, OpDefs.O_ST5, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0297 */ new UdItabEntry( UdMnemonicCode.UD_Ifcmovnu, OpDefs.O_ST0, OpDefs.O_ST6, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0298 */ new UdItabEntry( UdMnemonicCode.UD_Ifcmovnu, OpDefs.O_ST0, OpDefs.O_ST7, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0299 */ new UdItabEntry( UdMnemonicCode.UD_Ifucomi, OpDefs.O_ST0, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0300 */ new UdItabEntry( UdMnemonicCode.UD_Ifucomi, OpDefs.O_ST0, OpDefs.O_ST1, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0301 */ new UdItabEntry( UdMnemonicCode.UD_Ifucomi, OpDefs.O_ST0, OpDefs.O_ST2, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0302 */ new UdItabEntry( UdMnemonicCode.UD_Ifucomi, OpDefs.O_ST0, OpDefs.O_ST3, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0303 */ new UdItabEntry( UdMnemonicCode.UD_Ifucomi, OpDefs.O_ST0, OpDefs.O_ST4, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0304 */ new UdItabEntry( UdMnemonicCode.UD_Ifucomi, OpDefs.O_ST0, OpDefs.O_ST5, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0305 */ new UdItabEntry( UdMnemonicCode.UD_Ifucomi, OpDefs.O_ST0, OpDefs.O_ST6, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0306 */ new UdItabEntry( UdMnemonicCode.UD_Ifucomi, OpDefs.O_ST0, OpDefs.O_ST7, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0307 */ new UdItabEntry( UdMnemonicCode.UD_Ifcom, OpDefs.O_Md, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0308 */ new UdItabEntry( UdMnemonicCode.UD_Ifcom, OpDefs.O_Mq, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0309 */ new UdItabEntry( UdMnemonicCode.UD_Ifcom, OpDefs.O_ST0, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0310 */ new UdItabEntry( UdMnemonicCode.UD_Ifcom, OpDefs.O_ST0, OpDefs.O_ST1, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0311 */ new UdItabEntry( UdMnemonicCode.UD_Ifcom, OpDefs.O_ST0, OpDefs.O_ST2, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0312 */ new UdItabEntry( UdMnemonicCode.UD_Ifcom, OpDefs.O_ST0, OpDefs.O_ST3, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0313 */ new UdItabEntry( UdMnemonicCode.UD_Ifcom, OpDefs.O_ST0, OpDefs.O_ST4, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0314 */ new UdItabEntry( UdMnemonicCode.UD_Ifcom, OpDefs.O_ST0, OpDefs.O_ST5, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0315 */ new UdItabEntry( UdMnemonicCode.UD_Ifcom, OpDefs.O_ST0, OpDefs.O_ST6, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0316 */ new UdItabEntry( UdMnemonicCode.UD_Ifcom, OpDefs.O_ST0, OpDefs.O_ST7, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0317 */ new UdItabEntry( UdMnemonicCode.UD_Ifcom2, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0318 */ new UdItabEntry( UdMnemonicCode.UD_Ifcom2, OpDefs.O_ST1, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0319 */ new UdItabEntry( UdMnemonicCode.UD_Ifcom2, OpDefs.O_ST2, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0320 */ new UdItabEntry( UdMnemonicCode.UD_Ifcom2, OpDefs.O_ST3, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0321 */ new UdItabEntry( UdMnemonicCode.UD_Ifcom2, OpDefs.O_ST4, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0322 */ new UdItabEntry( UdMnemonicCode.UD_Ifcom2, OpDefs.O_ST5, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0323 */ new UdItabEntry( UdMnemonicCode.UD_Ifcom2, OpDefs.O_ST6, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0324 */ new UdItabEntry( UdMnemonicCode.UD_Ifcom2, OpDefs.O_ST7, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0325 */ new UdItabEntry( UdMnemonicCode.UD_Ifcomp3, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0326 */ new UdItabEntry( UdMnemonicCode.UD_Ifcomp3, OpDefs.O_ST1, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0327 */ new UdItabEntry( UdMnemonicCode.UD_Ifcomp3, OpDefs.O_ST2, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0328 */ new UdItabEntry( UdMnemonicCode.UD_Ifcomp3, OpDefs.O_ST3, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0329 */ new UdItabEntry( UdMnemonicCode.UD_Ifcomp3, OpDefs.O_ST4, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0330 */ new UdItabEntry( UdMnemonicCode.UD_Ifcomp3, OpDefs.O_ST5, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0331 */ new UdItabEntry( UdMnemonicCode.UD_Ifcomp3, OpDefs.O_ST6, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0332 */ new UdItabEntry( UdMnemonicCode.UD_Ifcomp3, OpDefs.O_ST7, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0333 */ new UdItabEntry( UdMnemonicCode.UD_Ifcomi, OpDefs.O_ST0, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0334 */ new UdItabEntry( UdMnemonicCode.UD_Ifcomi, OpDefs.O_ST0, OpDefs.O_ST1, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0335 */ new UdItabEntry( UdMnemonicCode.UD_Ifcomi, OpDefs.O_ST0, OpDefs.O_ST2, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0336 */ new UdItabEntry( UdMnemonicCode.UD_Ifcomi, OpDefs.O_ST0, OpDefs.O_ST3, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0337 */ new UdItabEntry( UdMnemonicCode.UD_Ifcomi, OpDefs.O_ST0, OpDefs.O_ST4, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0338 */ new UdItabEntry( UdMnemonicCode.UD_Ifcomi, OpDefs.O_ST0, OpDefs.O_ST5, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0339 */ new UdItabEntry( UdMnemonicCode.UD_Ifcomi, OpDefs.O_ST0, OpDefs.O_ST6, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0340 */ new UdItabEntry( UdMnemonicCode.UD_Ifcomi, OpDefs.O_ST0, OpDefs.O_ST7, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0341 */ new UdItabEntry( UdMnemonicCode.UD_Ifucomip, OpDefs.O_ST0, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0342 */ new UdItabEntry( UdMnemonicCode.UD_Ifucomip, OpDefs.O_ST0, OpDefs.O_ST1, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0343 */ new UdItabEntry( UdMnemonicCode.UD_Ifucomip, OpDefs.O_ST0, OpDefs.O_ST2, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0344 */ new UdItabEntry( UdMnemonicCode.UD_Ifucomip, OpDefs.O_ST0, OpDefs.O_ST3, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0345 */ new UdItabEntry( UdMnemonicCode.UD_Ifucomip, OpDefs.O_ST0, OpDefs.O_ST4, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0346 */ new UdItabEntry( UdMnemonicCode.UD_Ifucomip, OpDefs.O_ST0, OpDefs.O_ST5, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0347 */ new UdItabEntry( UdMnemonicCode.UD_Ifucomip, OpDefs.O_ST0, OpDefs.O_ST6, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0348 */ new UdItabEntry( UdMnemonicCode.UD_Ifucomip, OpDefs.O_ST0, OpDefs.O_ST7, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0349 */ new UdItabEntry( UdMnemonicCode.UD_Ifcomip, OpDefs.O_ST0, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0350 */ new UdItabEntry( UdMnemonicCode.UD_Ifcomip, OpDefs.O_ST0, OpDefs.O_ST1, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0351 */ new UdItabEntry( UdMnemonicCode.UD_Ifcomip, OpDefs.O_ST0, OpDefs.O_ST2, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0352 */ new UdItabEntry( UdMnemonicCode.UD_Ifcomip, OpDefs.O_ST0, OpDefs.O_ST3, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0353 */ new UdItabEntry( UdMnemonicCode.UD_Ifcomip, OpDefs.O_ST0, OpDefs.O_ST4, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0354 */ new UdItabEntry( UdMnemonicCode.UD_Ifcomip, OpDefs.O_ST0, OpDefs.O_ST5, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0355 */ new UdItabEntry( UdMnemonicCode.UD_Ifcomip, OpDefs.O_ST0, OpDefs.O_ST6, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0356 */ new UdItabEntry( UdMnemonicCode.UD_Ifcomip, OpDefs.O_ST0, OpDefs.O_ST7, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0357 */ new UdItabEntry( UdMnemonicCode.UD_Ifcomp, OpDefs.O_Md, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0358 */ new UdItabEntry( UdMnemonicCode.UD_Ifcomp, OpDefs.O_Mq, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0359 */ new UdItabEntry( UdMnemonicCode.UD_Ifcomp, OpDefs.O_ST0, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0360 */ new UdItabEntry( UdMnemonicCode.UD_Ifcomp, OpDefs.O_ST0, OpDefs.O_ST1, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0361 */ new UdItabEntry( UdMnemonicCode.UD_Ifcomp, OpDefs.O_ST0, OpDefs.O_ST2, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0362 */ new UdItabEntry( UdMnemonicCode.UD_Ifcomp, OpDefs.O_ST0, OpDefs.O_ST3, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0363 */ new UdItabEntry( UdMnemonicCode.UD_Ifcomp, OpDefs.O_ST0, OpDefs.O_ST4, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0364 */ new UdItabEntry( UdMnemonicCode.UD_Ifcomp, OpDefs.O_ST0, OpDefs.O_ST5, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0365 */ new UdItabEntry( UdMnemonicCode.UD_Ifcomp, OpDefs.O_ST0, OpDefs.O_ST6, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0366 */ new UdItabEntry( UdMnemonicCode.UD_Ifcomp, OpDefs.O_ST0, OpDefs.O_ST7, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0367 */ new UdItabEntry( UdMnemonicCode.UD_Ifcomp5, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0368 */ new UdItabEntry( UdMnemonicCode.UD_Ifcomp5, OpDefs.O_ST1, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0369 */ new UdItabEntry( UdMnemonicCode.UD_Ifcomp5, OpDefs.O_ST2, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0370 */ new UdItabEntry( UdMnemonicCode.UD_Ifcomp5, OpDefs.O_ST3, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0371 */ new UdItabEntry( UdMnemonicCode.UD_Ifcomp5, OpDefs.O_ST4, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0372 */ new UdItabEntry( UdMnemonicCode.UD_Ifcomp5, OpDefs.O_ST5, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0373 */ new UdItabEntry( UdMnemonicCode.UD_Ifcomp5, OpDefs.O_ST6, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0374 */ new UdItabEntry( UdMnemonicCode.UD_Ifcomp5, OpDefs.O_ST7, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0375 */ new UdItabEntry( UdMnemonicCode.UD_Ifcompp, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0376 */ new UdItabEntry( UdMnemonicCode.UD_Ifcos, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0377 */ new UdItabEntry( UdMnemonicCode.UD_Ifdecstp, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0378 */ new UdItabEntry( UdMnemonicCode.UD_Ifdiv, OpDefs.O_Mq, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0379 */ new UdItabEntry( UdMnemonicCode.UD_Ifdiv, OpDefs.O_ST0, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0380 */ new UdItabEntry( UdMnemonicCode.UD_Ifdiv, OpDefs.O_ST1, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0381 */ new UdItabEntry( UdMnemonicCode.UD_Ifdiv, OpDefs.O_ST2, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0382 */ new UdItabEntry( UdMnemonicCode.UD_Ifdiv, OpDefs.O_ST3, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0383 */ new UdItabEntry( UdMnemonicCode.UD_Ifdiv, OpDefs.O_ST4, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0384 */ new UdItabEntry( UdMnemonicCode.UD_Ifdiv, OpDefs.O_ST5, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0385 */ new UdItabEntry( UdMnemonicCode.UD_Ifdiv, OpDefs.O_ST6, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0386 */ new UdItabEntry( UdMnemonicCode.UD_Ifdiv, OpDefs.O_ST7, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0387 */ new UdItabEntry( UdMnemonicCode.UD_Ifdiv, OpDefs.O_Md, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0388 */ new UdItabEntry( UdMnemonicCode.UD_Ifdiv, OpDefs.O_ST0, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0389 */ new UdItabEntry( UdMnemonicCode.UD_Ifdiv, OpDefs.O_ST0, OpDefs.O_ST1, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0390 */ new UdItabEntry( UdMnemonicCode.UD_Ifdiv, OpDefs.O_ST0, OpDefs.O_ST2, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0391 */ new UdItabEntry( UdMnemonicCode.UD_Ifdiv, OpDefs.O_ST0, OpDefs.O_ST3, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0392 */ new UdItabEntry( UdMnemonicCode.UD_Ifdiv, OpDefs.O_ST0, OpDefs.O_ST4, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0393 */ new UdItabEntry( UdMnemonicCode.UD_Ifdiv, OpDefs.O_ST0, OpDefs.O_ST5, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0394 */ new UdItabEntry( UdMnemonicCode.UD_Ifdiv, OpDefs.O_ST0, OpDefs.O_ST6, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0395 */ new UdItabEntry( UdMnemonicCode.UD_Ifdiv, OpDefs.O_ST0, OpDefs.O_ST7, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0396 */ new UdItabEntry( UdMnemonicCode.UD_Ifdivp, OpDefs.O_ST0, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0397 */ new UdItabEntry( UdMnemonicCode.UD_Ifdivp, OpDefs.O_ST1, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0398 */ new UdItabEntry( UdMnemonicCode.UD_Ifdivp, OpDefs.O_ST2, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0399 */ new UdItabEntry( UdMnemonicCode.UD_Ifdivp, OpDefs.O_ST3, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0400 */ new UdItabEntry( UdMnemonicCode.UD_Ifdivp, OpDefs.O_ST4, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0401 */ new UdItabEntry( UdMnemonicCode.UD_Ifdivp, OpDefs.O_ST5, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0402 */ new UdItabEntry( UdMnemonicCode.UD_Ifdivp, OpDefs.O_ST6, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0403 */ new UdItabEntry( UdMnemonicCode.UD_Ifdivp, OpDefs.O_ST7, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0404 */ new UdItabEntry( UdMnemonicCode.UD_Ifdivr, OpDefs.O_Mq, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0405 */ new UdItabEntry( UdMnemonicCode.UD_Ifdivr, OpDefs.O_ST0, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0406 */ new UdItabEntry( UdMnemonicCode.UD_Ifdivr, OpDefs.O_ST1, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0407 */ new UdItabEntry( UdMnemonicCode.UD_Ifdivr, OpDefs.O_ST2, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0408 */ new UdItabEntry( UdMnemonicCode.UD_Ifdivr, OpDefs.O_ST3, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0409 */ new UdItabEntry( UdMnemonicCode.UD_Ifdivr, OpDefs.O_ST4, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0410 */ new UdItabEntry( UdMnemonicCode.UD_Ifdivr, OpDefs.O_ST5, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0411 */ new UdItabEntry( UdMnemonicCode.UD_Ifdivr, OpDefs.O_ST6, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0412 */ new UdItabEntry( UdMnemonicCode.UD_Ifdivr, OpDefs.O_ST7, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0413 */ new UdItabEntry( UdMnemonicCode.UD_Ifdivr, OpDefs.O_Md, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0414 */ new UdItabEntry( UdMnemonicCode.UD_Ifdivr, OpDefs.O_ST0, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0415 */ new UdItabEntry( UdMnemonicCode.UD_Ifdivr, OpDefs.O_ST0, OpDefs.O_ST1, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0416 */ new UdItabEntry( UdMnemonicCode.UD_Ifdivr, OpDefs.O_ST0, OpDefs.O_ST2, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0417 */ new UdItabEntry( UdMnemonicCode.UD_Ifdivr, OpDefs.O_ST0, OpDefs.O_ST3, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0418 */ new UdItabEntry( UdMnemonicCode.UD_Ifdivr, OpDefs.O_ST0, OpDefs.O_ST4, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0419 */ new UdItabEntry( UdMnemonicCode.UD_Ifdivr, OpDefs.O_ST0, OpDefs.O_ST5, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0420 */ new UdItabEntry( UdMnemonicCode.UD_Ifdivr, OpDefs.O_ST0, OpDefs.O_ST6, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0421 */ new UdItabEntry( UdMnemonicCode.UD_Ifdivr, OpDefs.O_ST0, OpDefs.O_ST7, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0422 */ new UdItabEntry( UdMnemonicCode.UD_Ifdivrp, OpDefs.O_ST0, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0423 */ new UdItabEntry( UdMnemonicCode.UD_Ifdivrp, OpDefs.O_ST1, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0424 */ new UdItabEntry( UdMnemonicCode.UD_Ifdivrp, OpDefs.O_ST2, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0425 */ new UdItabEntry( UdMnemonicCode.UD_Ifdivrp, OpDefs.O_ST3, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0426 */ new UdItabEntry( UdMnemonicCode.UD_Ifdivrp, OpDefs.O_ST4, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0427 */ new UdItabEntry( UdMnemonicCode.UD_Ifdivrp, OpDefs.O_ST5, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0428 */ new UdItabEntry( UdMnemonicCode.UD_Ifdivrp, OpDefs.O_ST6, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0429 */ new UdItabEntry( UdMnemonicCode.UD_Ifdivrp, OpDefs.O_ST7, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0430 */ new UdItabEntry( UdMnemonicCode.UD_Ifemms, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0431 */ new UdItabEntry( UdMnemonicCode.UD_Iffree, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0432 */ new UdItabEntry( UdMnemonicCode.UD_Iffree, OpDefs.O_ST1, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0433 */ new UdItabEntry( UdMnemonicCode.UD_Iffree, OpDefs.O_ST2, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0434 */ new UdItabEntry( UdMnemonicCode.UD_Iffree, OpDefs.O_ST3, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0435 */ new UdItabEntry( UdMnemonicCode.UD_Iffree, OpDefs.O_ST4, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0436 */ new UdItabEntry( UdMnemonicCode.UD_Iffree, OpDefs.O_ST5, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0437 */ new UdItabEntry( UdMnemonicCode.UD_Iffree, OpDefs.O_ST6, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0438 */ new UdItabEntry( UdMnemonicCode.UD_Iffree, OpDefs.O_ST7, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0439 */ new UdItabEntry( UdMnemonicCode.UD_Iffreep, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0440 */ new UdItabEntry( UdMnemonicCode.UD_Iffreep, OpDefs.O_ST1, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0441 */ new UdItabEntry( UdMnemonicCode.UD_Iffreep, OpDefs.O_ST2, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0442 */ new UdItabEntry( UdMnemonicCode.UD_Iffreep, OpDefs.O_ST3, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0443 */ new UdItabEntry( UdMnemonicCode.UD_Iffreep, OpDefs.O_ST4, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0444 */ new UdItabEntry( UdMnemonicCode.UD_Iffreep, OpDefs.O_ST5, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0445 */ new UdItabEntry( UdMnemonicCode.UD_Iffreep, OpDefs.O_ST6, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0446 */ new UdItabEntry( UdMnemonicCode.UD_Iffreep, OpDefs.O_ST7, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0447 */ new UdItabEntry( UdMnemonicCode.UD_Ificom, OpDefs.O_Mw, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0448 */ new UdItabEntry( UdMnemonicCode.UD_Ificom, OpDefs.O_Md, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0449 */ new UdItabEntry( UdMnemonicCode.UD_Ificomp, OpDefs.O_Mw, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0450 */ new UdItabEntry( UdMnemonicCode.UD_Ificomp, OpDefs.O_Md, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0451 */ new UdItabEntry( UdMnemonicCode.UD_Ifild, OpDefs.O_Mw, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0452 */ new UdItabEntry( UdMnemonicCode.UD_Ifild, OpDefs.O_Mq, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0453 */ new UdItabEntry( UdMnemonicCode.UD_Ifild, OpDefs.O_Md, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0454 */ new UdItabEntry( UdMnemonicCode.UD_Ifincstp, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0455 */ new UdItabEntry( UdMnemonicCode.UD_Ifninit, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0456 */ new UdItabEntry( UdMnemonicCode.UD_Ifiadd, OpDefs.O_Md, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0457 */ new UdItabEntry( UdMnemonicCode.UD_Ifiadd, OpDefs.O_Mw, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0458 */ new UdItabEntry( UdMnemonicCode.UD_Ifidivr, OpDefs.O_Md, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0459 */ new UdItabEntry( UdMnemonicCode.UD_Ifidivr, OpDefs.O_Mw, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0460 */ new UdItabEntry( UdMnemonicCode.UD_Ifidiv, OpDefs.O_Md, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0461 */ new UdItabEntry( UdMnemonicCode.UD_Ifidiv, OpDefs.O_Mw, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0462 */ new UdItabEntry( UdMnemonicCode.UD_Ifisub, OpDefs.O_Md, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0463 */ new UdItabEntry( UdMnemonicCode.UD_Ifisub, OpDefs.O_Mw, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0464 */ new UdItabEntry( UdMnemonicCode.UD_Ifisubr, OpDefs.O_Md, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0465 */ new UdItabEntry( UdMnemonicCode.UD_Ifisubr, OpDefs.O_Mw, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0466 */ new UdItabEntry( UdMnemonicCode.UD_Ifist, OpDefs.O_Mw, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0467 */ new UdItabEntry( UdMnemonicCode.UD_Ifist, OpDefs.O_Md, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0468 */ new UdItabEntry( UdMnemonicCode.UD_Ifistp, OpDefs.O_Mw, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0469 */ new UdItabEntry( UdMnemonicCode.UD_Ifistp, OpDefs.O_Mq, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0470 */ new UdItabEntry( UdMnemonicCode.UD_Ifistp, OpDefs.O_Md, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0471 */ new UdItabEntry( UdMnemonicCode.UD_Ifisttp, OpDefs.O_Md, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0472 */ new UdItabEntry( UdMnemonicCode.UD_Ifisttp, OpDefs.O_Mq, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0473 */ new UdItabEntry( UdMnemonicCode.UD_Ifisttp, OpDefs.O_Mw, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0474 */ new UdItabEntry( UdMnemonicCode.UD_Ifld, OpDefs.O_Mt, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0475 */ new UdItabEntry( UdMnemonicCode.UD_Ifld, OpDefs.O_Mq, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0476 */ new UdItabEntry( UdMnemonicCode.UD_Ifld, OpDefs.O_Md, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0477 */ new UdItabEntry( UdMnemonicCode.UD_Ifld, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0478 */ new UdItabEntry( UdMnemonicCode.UD_Ifld, OpDefs.O_ST1, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0479 */ new UdItabEntry( UdMnemonicCode.UD_Ifld, OpDefs.O_ST2, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0480 */ new UdItabEntry( UdMnemonicCode.UD_Ifld, OpDefs.O_ST3, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0481 */ new UdItabEntry( UdMnemonicCode.UD_Ifld, OpDefs.O_ST4, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0482 */ new UdItabEntry( UdMnemonicCode.UD_Ifld, OpDefs.O_ST5, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0483 */ new UdItabEntry( UdMnemonicCode.UD_Ifld, OpDefs.O_ST6, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0484 */ new UdItabEntry( UdMnemonicCode.UD_Ifld, OpDefs.O_ST7, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0485 */ new UdItabEntry( UdMnemonicCode.UD_Ifld1, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0486 */ new UdItabEntry( UdMnemonicCode.UD_Ifldl2t, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0487 */ new UdItabEntry( UdMnemonicCode.UD_Ifldl2e, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0488 */ new UdItabEntry( UdMnemonicCode.UD_Ifldpi, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0489 */ new UdItabEntry( UdMnemonicCode.UD_Ifldlg2, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0490 */ new UdItabEntry( UdMnemonicCode.UD_Ifldln2, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0491 */ new UdItabEntry( UdMnemonicCode.UD_Ifldz, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0492 */ new UdItabEntry( UdMnemonicCode.UD_Ifldcw, OpDefs.O_Mw, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0493 */ new UdItabEntry( UdMnemonicCode.UD_Ifldenv, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0494 */ new UdItabEntry( UdMnemonicCode.UD_Ifmul, OpDefs.O_Mq, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0495 */ new UdItabEntry( UdMnemonicCode.UD_Ifmul, OpDefs.O_ST0, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0496 */ new UdItabEntry( UdMnemonicCode.UD_Ifmul, OpDefs.O_ST1, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0497 */ new UdItabEntry( UdMnemonicCode.UD_Ifmul, OpDefs.O_ST2, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0498 */ new UdItabEntry( UdMnemonicCode.UD_Ifmul, OpDefs.O_ST3, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0499 */ new UdItabEntry( UdMnemonicCode.UD_Ifmul, OpDefs.O_ST4, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0500 */ new UdItabEntry( UdMnemonicCode.UD_Ifmul, OpDefs.O_ST5, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0501 */ new UdItabEntry( UdMnemonicCode.UD_Ifmul, OpDefs.O_ST6, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0502 */ new UdItabEntry( UdMnemonicCode.UD_Ifmul, OpDefs.O_ST7, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0503 */ new UdItabEntry( UdMnemonicCode.UD_Ifmul, OpDefs.O_Md, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0504 */ new UdItabEntry( UdMnemonicCode.UD_Ifmul, OpDefs.O_ST0, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0505 */ new UdItabEntry( UdMnemonicCode.UD_Ifmul, OpDefs.O_ST0, OpDefs.O_ST1, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0506 */ new UdItabEntry( UdMnemonicCode.UD_Ifmul, OpDefs.O_ST0, OpDefs.O_ST2, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0507 */ new UdItabEntry( UdMnemonicCode.UD_Ifmul, OpDefs.O_ST0, OpDefs.O_ST3, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0508 */ new UdItabEntry( UdMnemonicCode.UD_Ifmul, OpDefs.O_ST0, OpDefs.O_ST4, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0509 */ new UdItabEntry( UdMnemonicCode.UD_Ifmul, OpDefs.O_ST0, OpDefs.O_ST5, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0510 */ new UdItabEntry( UdMnemonicCode.UD_Ifmul, OpDefs.O_ST0, OpDefs.O_ST6, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0511 */ new UdItabEntry( UdMnemonicCode.UD_Ifmul, OpDefs.O_ST0, OpDefs.O_ST7, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0512 */ new UdItabEntry( UdMnemonicCode.UD_Ifmulp, OpDefs.O_ST0, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0513 */ new UdItabEntry( UdMnemonicCode.UD_Ifmulp, OpDefs.O_ST1, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0514 */ new UdItabEntry( UdMnemonicCode.UD_Ifmulp, OpDefs.O_ST2, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0515 */ new UdItabEntry( UdMnemonicCode.UD_Ifmulp, OpDefs.O_ST3, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0516 */ new UdItabEntry( UdMnemonicCode.UD_Ifmulp, OpDefs.O_ST4, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0517 */ new UdItabEntry( UdMnemonicCode.UD_Ifmulp, OpDefs.O_ST5, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0518 */ new UdItabEntry( UdMnemonicCode.UD_Ifmulp, OpDefs.O_ST6, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0519 */ new UdItabEntry( UdMnemonicCode.UD_Ifmulp, OpDefs.O_ST7, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0520 */ new UdItabEntry( UdMnemonicCode.UD_Ifimul, OpDefs.O_Md, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0521 */ new UdItabEntry( UdMnemonicCode.UD_Ifimul, OpDefs.O_Mw, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0522 */ new UdItabEntry( UdMnemonicCode.UD_Ifnop, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0523 */ new UdItabEntry( UdMnemonicCode.UD_Ifndisi, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0524 */ new UdItabEntry( UdMnemonicCode.UD_Ifneni, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0525 */ new UdItabEntry( UdMnemonicCode.UD_Ifnsetpm, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0526 */ new UdItabEntry( UdMnemonicCode.UD_Ifpatan, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0527 */ new UdItabEntry( UdMnemonicCode.UD_Ifprem, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0528 */ new UdItabEntry( UdMnemonicCode.UD_Ifprem1, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0529 */ new UdItabEntry( UdMnemonicCode.UD_Ifptan, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0530 */ new UdItabEntry( UdMnemonicCode.UD_Ifrndint, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0531 */ new UdItabEntry( UdMnemonicCode.UD_Ifrstor, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0532 */ new UdItabEntry( UdMnemonicCode.UD_Ifrstpm, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0533 */ new UdItabEntry( UdMnemonicCode.UD_Ifnsave, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0534 */ new UdItabEntry( UdMnemonicCode.UD_Ifscale, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0535 */ new UdItabEntry( UdMnemonicCode.UD_Ifsin, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0536 */ new UdItabEntry( UdMnemonicCode.UD_Ifsincos, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0537 */ new UdItabEntry( UdMnemonicCode.UD_Ifsqrt, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0538 */ new UdItabEntry( UdMnemonicCode.UD_Ifstp, OpDefs.O_Mt, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0539 */ new UdItabEntry( UdMnemonicCode.UD_Ifstp, OpDefs.O_Mq, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0540 */ new UdItabEntry( UdMnemonicCode.UD_Ifstp, OpDefs.O_Md, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0541 */ new UdItabEntry( UdMnemonicCode.UD_Ifstp, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0542 */ new UdItabEntry( UdMnemonicCode.UD_Ifstp, OpDefs.O_ST1, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0543 */ new UdItabEntry( UdMnemonicCode.UD_Ifstp, OpDefs.O_ST2, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0544 */ new UdItabEntry( UdMnemonicCode.UD_Ifstp, OpDefs.O_ST3, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0545 */ new UdItabEntry( UdMnemonicCode.UD_Ifstp, OpDefs.O_ST4, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0546 */ new UdItabEntry( UdMnemonicCode.UD_Ifstp, OpDefs.O_ST5, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0547 */ new UdItabEntry( UdMnemonicCode.UD_Ifstp, OpDefs.O_ST6, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0548 */ new UdItabEntry( UdMnemonicCode.UD_Ifstp, OpDefs.O_ST7, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0549 */ new UdItabEntry( UdMnemonicCode.UD_Ifstp1, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0550 */ new UdItabEntry( UdMnemonicCode.UD_Ifstp1, OpDefs.O_ST1, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0551 */ new UdItabEntry( UdMnemonicCode.UD_Ifstp1, OpDefs.O_ST2, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0552 */ new UdItabEntry( UdMnemonicCode.UD_Ifstp1, OpDefs.O_ST3, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0553 */ new UdItabEntry( UdMnemonicCode.UD_Ifstp1, OpDefs.O_ST4, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0554 */ new UdItabEntry( UdMnemonicCode.UD_Ifstp1, OpDefs.O_ST5, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0555 */ new UdItabEntry( UdMnemonicCode.UD_Ifstp1, OpDefs.O_ST6, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0556 */ new UdItabEntry( UdMnemonicCode.UD_Ifstp1, OpDefs.O_ST7, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0557 */ new UdItabEntry( UdMnemonicCode.UD_Ifstp8, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0558 */ new UdItabEntry( UdMnemonicCode.UD_Ifstp8, OpDefs.O_ST1, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0559 */ new UdItabEntry( UdMnemonicCode.UD_Ifstp8, OpDefs.O_ST2, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0560 */ new UdItabEntry( UdMnemonicCode.UD_Ifstp8, OpDefs.O_ST3, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0561 */ new UdItabEntry( UdMnemonicCode.UD_Ifstp8, OpDefs.O_ST4, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0562 */ new UdItabEntry( UdMnemonicCode.UD_Ifstp8, OpDefs.O_ST5, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0563 */ new UdItabEntry( UdMnemonicCode.UD_Ifstp8, OpDefs.O_ST6, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0564 */ new UdItabEntry( UdMnemonicCode.UD_Ifstp8, OpDefs.O_ST7, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0565 */ new UdItabEntry( UdMnemonicCode.UD_Ifstp9, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0566 */ new UdItabEntry( UdMnemonicCode.UD_Ifstp9, OpDefs.O_ST1, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0567 */ new UdItabEntry( UdMnemonicCode.UD_Ifstp9, OpDefs.O_ST2, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0568 */ new UdItabEntry( UdMnemonicCode.UD_Ifstp9, OpDefs.O_ST3, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0569 */ new UdItabEntry( UdMnemonicCode.UD_Ifstp9, OpDefs.O_ST4, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0570 */ new UdItabEntry( UdMnemonicCode.UD_Ifstp9, OpDefs.O_ST5, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0571 */ new UdItabEntry( UdMnemonicCode.UD_Ifstp9, OpDefs.O_ST6, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0572 */ new UdItabEntry( UdMnemonicCode.UD_Ifstp9, OpDefs.O_ST7, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0573 */ new UdItabEntry( UdMnemonicCode.UD_Ifst, OpDefs.O_Md, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0574 */ new UdItabEntry( UdMnemonicCode.UD_Ifst, OpDefs.O_Mq, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0575 */ new UdItabEntry( UdMnemonicCode.UD_Ifst, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0576 */ new UdItabEntry( UdMnemonicCode.UD_Ifst, OpDefs.O_ST1, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0577 */ new UdItabEntry( UdMnemonicCode.UD_Ifst, OpDefs.O_ST2, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0578 */ new UdItabEntry( UdMnemonicCode.UD_Ifst, OpDefs.O_ST3, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0579 */ new UdItabEntry( UdMnemonicCode.UD_Ifst, OpDefs.O_ST4, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0580 */ new UdItabEntry( UdMnemonicCode.UD_Ifst, OpDefs.O_ST5, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0581 */ new UdItabEntry( UdMnemonicCode.UD_Ifst, OpDefs.O_ST6, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0582 */ new UdItabEntry( UdMnemonicCode.UD_Ifst, OpDefs.O_ST7, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0583 */ new UdItabEntry( UdMnemonicCode.UD_Ifnstcw, OpDefs.O_Mw, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0584 */ new UdItabEntry( UdMnemonicCode.UD_Ifnstenv, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0585 */ new UdItabEntry( UdMnemonicCode.UD_Ifnstsw, OpDefs.O_Mw, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0586 */ new UdItabEntry( UdMnemonicCode.UD_Ifnstsw, OpDefs.O_AX, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0587 */ new UdItabEntry( UdMnemonicCode.UD_Ifsub, OpDefs.O_Md, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0588 */ new UdItabEntry( UdMnemonicCode.UD_Ifsub, OpDefs.O_Mq, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0589 */ new UdItabEntry( UdMnemonicCode.UD_Ifsub, OpDefs.O_ST0, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0590 */ new UdItabEntry( UdMnemonicCode.UD_Ifsub, OpDefs.O_ST0, OpDefs.O_ST1, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0591 */ new UdItabEntry( UdMnemonicCode.UD_Ifsub, OpDefs.O_ST0, OpDefs.O_ST2, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0592 */ new UdItabEntry( UdMnemonicCode.UD_Ifsub, OpDefs.O_ST0, OpDefs.O_ST3, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0593 */ new UdItabEntry( UdMnemonicCode.UD_Ifsub, OpDefs.O_ST0, OpDefs.O_ST4, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0594 */ new UdItabEntry( UdMnemonicCode.UD_Ifsub, OpDefs.O_ST0, OpDefs.O_ST5, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0595 */ new UdItabEntry( UdMnemonicCode.UD_Ifsub, OpDefs.O_ST0, OpDefs.O_ST6, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0596 */ new UdItabEntry( UdMnemonicCode.UD_Ifsub, OpDefs.O_ST0, OpDefs.O_ST7, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0597 */ new UdItabEntry( UdMnemonicCode.UD_Ifsub, OpDefs.O_ST0, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0598 */ new UdItabEntry( UdMnemonicCode.UD_Ifsub, OpDefs.O_ST1, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0599 */ new UdItabEntry( UdMnemonicCode.UD_Ifsub, OpDefs.O_ST2, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0600 */ new UdItabEntry( UdMnemonicCode.UD_Ifsub, OpDefs.O_ST3, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0601 */ new UdItabEntry( UdMnemonicCode.UD_Ifsub, OpDefs.O_ST4, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0602 */ new UdItabEntry( UdMnemonicCode.UD_Ifsub, OpDefs.O_ST5, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0603 */ new UdItabEntry( UdMnemonicCode.UD_Ifsub, OpDefs.O_ST6, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0604 */ new UdItabEntry( UdMnemonicCode.UD_Ifsub, OpDefs.O_ST7, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0605 */ new UdItabEntry( UdMnemonicCode.UD_Ifsubp, OpDefs.O_ST0, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0606 */ new UdItabEntry( UdMnemonicCode.UD_Ifsubp, OpDefs.O_ST1, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0607 */ new UdItabEntry( UdMnemonicCode.UD_Ifsubp, OpDefs.O_ST2, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0608 */ new UdItabEntry( UdMnemonicCode.UD_Ifsubp, OpDefs.O_ST3, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0609 */ new UdItabEntry( UdMnemonicCode.UD_Ifsubp, OpDefs.O_ST4, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0610 */ new UdItabEntry( UdMnemonicCode.UD_Ifsubp, OpDefs.O_ST5, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0611 */ new UdItabEntry( UdMnemonicCode.UD_Ifsubp, OpDefs.O_ST6, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0612 */ new UdItabEntry( UdMnemonicCode.UD_Ifsubp, OpDefs.O_ST7, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0613 */ new UdItabEntry( UdMnemonicCode.UD_Ifsubr, OpDefs.O_Mq, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0614 */ new UdItabEntry( UdMnemonicCode.UD_Ifsubr, OpDefs.O_ST0, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0615 */ new UdItabEntry( UdMnemonicCode.UD_Ifsubr, OpDefs.O_ST0, OpDefs.O_ST1, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0616 */ new UdItabEntry( UdMnemonicCode.UD_Ifsubr, OpDefs.O_ST0, OpDefs.O_ST2, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0617 */ new UdItabEntry( UdMnemonicCode.UD_Ifsubr, OpDefs.O_ST0, OpDefs.O_ST3, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0618 */ new UdItabEntry( UdMnemonicCode.UD_Ifsubr, OpDefs.O_ST0, OpDefs.O_ST4, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0619 */ new UdItabEntry( UdMnemonicCode.UD_Ifsubr, OpDefs.O_ST0, OpDefs.O_ST5, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0620 */ new UdItabEntry( UdMnemonicCode.UD_Ifsubr, OpDefs.O_ST0, OpDefs.O_ST6, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0621 */ new UdItabEntry( UdMnemonicCode.UD_Ifsubr, OpDefs.O_ST0, OpDefs.O_ST7, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0622 */ new UdItabEntry( UdMnemonicCode.UD_Ifsubr, OpDefs.O_ST0, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0623 */ new UdItabEntry( UdMnemonicCode.UD_Ifsubr, OpDefs.O_ST1, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0624 */ new UdItabEntry( UdMnemonicCode.UD_Ifsubr, OpDefs.O_ST2, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0625 */ new UdItabEntry( UdMnemonicCode.UD_Ifsubr, OpDefs.O_ST3, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0626 */ new UdItabEntry( UdMnemonicCode.UD_Ifsubr, OpDefs.O_ST4, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0627 */ new UdItabEntry( UdMnemonicCode.UD_Ifsubr, OpDefs.O_ST5, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0628 */ new UdItabEntry( UdMnemonicCode.UD_Ifsubr, OpDefs.O_ST6, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0629 */ new UdItabEntry( UdMnemonicCode.UD_Ifsubr, OpDefs.O_ST7, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0630 */ new UdItabEntry( UdMnemonicCode.UD_Ifsubr, OpDefs.O_Md, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0631 */ new UdItabEntry( UdMnemonicCode.UD_Ifsubrp, OpDefs.O_ST0, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0632 */ new UdItabEntry( UdMnemonicCode.UD_Ifsubrp, OpDefs.O_ST1, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0633 */ new UdItabEntry( UdMnemonicCode.UD_Ifsubrp, OpDefs.O_ST2, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0634 */ new UdItabEntry( UdMnemonicCode.UD_Ifsubrp, OpDefs.O_ST3, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0635 */ new UdItabEntry( UdMnemonicCode.UD_Ifsubrp, OpDefs.O_ST4, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0636 */ new UdItabEntry( UdMnemonicCode.UD_Ifsubrp, OpDefs.O_ST5, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0637 */ new UdItabEntry( UdMnemonicCode.UD_Ifsubrp, OpDefs.O_ST6, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0638 */ new UdItabEntry( UdMnemonicCode.UD_Ifsubrp, OpDefs.O_ST7, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0639 */ new UdItabEntry( UdMnemonicCode.UD_Iftst, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0640 */ new UdItabEntry( UdMnemonicCode.UD_Ifucom, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0641 */ new UdItabEntry( UdMnemonicCode.UD_Ifucom, OpDefs.O_ST1, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0642 */ new UdItabEntry( UdMnemonicCode.UD_Ifucom, OpDefs.O_ST2, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0643 */ new UdItabEntry( UdMnemonicCode.UD_Ifucom, OpDefs.O_ST3, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0644 */ new UdItabEntry( UdMnemonicCode.UD_Ifucom, OpDefs.O_ST4, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0645 */ new UdItabEntry( UdMnemonicCode.UD_Ifucom, OpDefs.O_ST5, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0646 */ new UdItabEntry( UdMnemonicCode.UD_Ifucom, OpDefs.O_ST6, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0647 */ new UdItabEntry( UdMnemonicCode.UD_Ifucom, OpDefs.O_ST7, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0648 */ new UdItabEntry( UdMnemonicCode.UD_Ifucomp, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0649 */ new UdItabEntry( UdMnemonicCode.UD_Ifucomp, OpDefs.O_ST1, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0650 */ new UdItabEntry( UdMnemonicCode.UD_Ifucomp, OpDefs.O_ST2, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0651 */ new UdItabEntry( UdMnemonicCode.UD_Ifucomp, OpDefs.O_ST3, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0652 */ new UdItabEntry( UdMnemonicCode.UD_Ifucomp, OpDefs.O_ST4, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0653 */ new UdItabEntry( UdMnemonicCode.UD_Ifucomp, OpDefs.O_ST5, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0654 */ new UdItabEntry( UdMnemonicCode.UD_Ifucomp, OpDefs.O_ST6, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0655 */ new UdItabEntry( UdMnemonicCode.UD_Ifucomp, OpDefs.O_ST7, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0656 */ new UdItabEntry( UdMnemonicCode.UD_Ifucompp, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0657 */ new UdItabEntry( UdMnemonicCode.UD_Ifxam, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0658 */ new UdItabEntry( UdMnemonicCode.UD_Ifxch, OpDefs.O_ST0, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0659 */ new UdItabEntry( UdMnemonicCode.UD_Ifxch, OpDefs.O_ST0, OpDefs.O_ST1, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0660 */ new UdItabEntry( UdMnemonicCode.UD_Ifxch, OpDefs.O_ST0, OpDefs.O_ST2, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0661 */ new UdItabEntry( UdMnemonicCode.UD_Ifxch, OpDefs.O_ST0, OpDefs.O_ST3, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0662 */ new UdItabEntry( UdMnemonicCode.UD_Ifxch, OpDefs.O_ST0, OpDefs.O_ST4, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0663 */ new UdItabEntry( UdMnemonicCode.UD_Ifxch, OpDefs.O_ST0, OpDefs.O_ST5, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0664 */ new UdItabEntry( UdMnemonicCode.UD_Ifxch, OpDefs.O_ST0, OpDefs.O_ST6, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0665 */ new UdItabEntry( UdMnemonicCode.UD_Ifxch, OpDefs.O_ST0, OpDefs.O_ST7, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0666 */ new UdItabEntry( UdMnemonicCode.UD_Ifxch4, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0667 */ new UdItabEntry( UdMnemonicCode.UD_Ifxch4, OpDefs.O_ST1, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0668 */ new UdItabEntry( UdMnemonicCode.UD_Ifxch4, OpDefs.O_ST2, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0669 */ new UdItabEntry( UdMnemonicCode.UD_Ifxch4, OpDefs.O_ST3, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0670 */ new UdItabEntry( UdMnemonicCode.UD_Ifxch4, OpDefs.O_ST4, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0671 */ new UdItabEntry( UdMnemonicCode.UD_Ifxch4, OpDefs.O_ST5, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0672 */ new UdItabEntry( UdMnemonicCode.UD_Ifxch4, OpDefs.O_ST6, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0673 */ new UdItabEntry( UdMnemonicCode.UD_Ifxch4, OpDefs.O_ST7, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0674 */ new UdItabEntry( UdMnemonicCode.UD_Ifxch7, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0675 */ new UdItabEntry( UdMnemonicCode.UD_Ifxch7, OpDefs.O_ST1, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0676 */ new UdItabEntry( UdMnemonicCode.UD_Ifxch7, OpDefs.O_ST2, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0677 */ new UdItabEntry( UdMnemonicCode.UD_Ifxch7, OpDefs.O_ST3, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0678 */ new UdItabEntry( UdMnemonicCode.UD_Ifxch7, OpDefs.O_ST4, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0679 */ new UdItabEntry( UdMnemonicCode.UD_Ifxch7, OpDefs.O_ST5, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0680 */ new UdItabEntry( UdMnemonicCode.UD_Ifxch7, OpDefs.O_ST6, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0681 */ new UdItabEntry( UdMnemonicCode.UD_Ifxch7, OpDefs.O_ST7, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0682 */ new UdItabEntry( UdMnemonicCode.UD_Ifxrstor, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0683 */ new UdItabEntry( UdMnemonicCode.UD_Ifxsave, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0684 */ new UdItabEntry( UdMnemonicCode.UD_Ifxtract, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0685 */ new UdItabEntry( UdMnemonicCode.UD_Ifyl2x, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0686 */ new UdItabEntry( UdMnemonicCode.UD_Ifyl2xp1, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0687 */ new UdItabEntry( UdMnemonicCode.UD_Ihlt, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0688 */ new UdItabEntry( UdMnemonicCode.UD_Iidiv, OpDefs.O_Ev, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0689 */ new UdItabEntry( UdMnemonicCode.UD_Iidiv, OpDefs.O_Eb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0690 */ new UdItabEntry( UdMnemonicCode.UD_Iin, OpDefs.O_AL, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0691 */ new UdItabEntry( UdMnemonicCode.UD_Iin, OpDefs.O_eAX, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso ),
            /* 0692 */ new UdItabEntry( UdMnemonicCode.UD_Iin, OpDefs.O_AL, OpDefs.O_DX, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0693 */ new UdItabEntry( UdMnemonicCode.UD_Iin, OpDefs.O_eAX, OpDefs.O_DX, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso ),
            /* 0694 */ new UdItabEntry( UdMnemonicCode.UD_Iimul, OpDefs.O_Gv, OpDefs.O_Ev, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0695 */ new UdItabEntry( UdMnemonicCode.UD_Iimul, OpDefs.O_Eb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0696 */ new UdItabEntry( UdMnemonicCode.UD_Iimul, OpDefs.O_Ev, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0697 */ new UdItabEntry( UdMnemonicCode.UD_Iimul, OpDefs.O_Gv, OpDefs.O_Ev, OpDefs.O_Iz, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0698 */ new UdItabEntry( UdMnemonicCode.UD_Iimul, OpDefs.O_Gv, OpDefs.O_Ev, OpDefs.O_sIb, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0699 */ new UdItabEntry( UdMnemonicCode.UD_Iinc, OpDefs.O_R0z, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso ),
            /* 0700 */ new UdItabEntry( UdMnemonicCode.UD_Iinc, OpDefs.O_R1z, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso ),
            /* 0701 */ new UdItabEntry( UdMnemonicCode.UD_Iinc, OpDefs.O_R2z, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso ),
            /* 0702 */ new UdItabEntry( UdMnemonicCode.UD_Iinc, OpDefs.O_R3z, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso ),
            /* 0703 */ new UdItabEntry( UdMnemonicCode.UD_Iinc, OpDefs.O_R4z, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso ),
            /* 0704 */ new UdItabEntry( UdMnemonicCode.UD_Iinc, OpDefs.O_R5z, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso ),
            /* 0705 */ new UdItabEntry( UdMnemonicCode.UD_Iinc, OpDefs.O_R6z, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso ),
            /* 0706 */ new UdItabEntry( UdMnemonicCode.UD_Iinc, OpDefs.O_R7z, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso ),
            /* 0707 */ new UdItabEntry( UdMnemonicCode.UD_Iinc, OpDefs.O_Ev, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0708 */ new UdItabEntry( UdMnemonicCode.UD_Iinc, OpDefs.O_Eb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0709 */ new UdItabEntry( UdMnemonicCode.UD_Iinsb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PStr|BitOps.PSeg ),
            /* 0710 */ new UdItabEntry( UdMnemonicCode.UD_Iinsw, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PStr|BitOps.POso|BitOps.PSeg ),
            /* 0711 */ new UdItabEntry( UdMnemonicCode.UD_Iinsd, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PStr|BitOps.POso|BitOps.PSeg ),
            /* 0712 */ new UdItabEntry( UdMnemonicCode.UD_Iint1, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0713 */ new UdItabEntry( UdMnemonicCode.UD_Iint3, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0714 */ new UdItabEntry( UdMnemonicCode.UD_Iint, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0715 */ new UdItabEntry( UdMnemonicCode.UD_Iinto, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PInv64 ),
            /* 0716 */ new UdItabEntry( UdMnemonicCode.UD_Iinvd, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0717 */ new UdItabEntry( UdMnemonicCode.UD_Iinvept, OpDefs.O_Gd, OpDefs.O_Mo, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0718 */ new UdItabEntry( UdMnemonicCode.UD_Iinvept, OpDefs.O_Gq, OpDefs.O_Mo, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0719 */ new UdItabEntry( UdMnemonicCode.UD_Iinvlpg, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0720 */ new UdItabEntry( UdMnemonicCode.UD_Iinvlpga, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0721 */ new UdItabEntry( UdMnemonicCode.UD_Iinvvpid, OpDefs.O_Gd, OpDefs.O_Mo, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0722 */ new UdItabEntry( UdMnemonicCode.UD_Iinvvpid, OpDefs.O_Gq, OpDefs.O_Mo, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0723 */ new UdItabEntry( UdMnemonicCode.UD_Iiretw, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso|BitOps.PRexw ),
            /* 0724 */ new UdItabEntry( UdMnemonicCode.UD_Iiretd, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso|BitOps.PRexw ),
            /* 0725 */ new UdItabEntry( UdMnemonicCode.UD_Iiretq, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso|BitOps.PRexw ),
            /* 0726 */ new UdItabEntry( UdMnemonicCode.UD_Ijo, OpDefs.O_Jb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0727 */ new UdItabEntry( UdMnemonicCode.UD_Ijo, OpDefs.O_Jz, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso|BitOps.PDef64 ),
            /* 0728 */ new UdItabEntry( UdMnemonicCode.UD_Ijno, OpDefs.O_Jb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0729 */ new UdItabEntry( UdMnemonicCode.UD_Ijno, OpDefs.O_Jz, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso|BitOps.PDef64 ),
            /* 0730 */ new UdItabEntry( UdMnemonicCode.UD_Ijb, OpDefs.O_Jb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0731 */ new UdItabEntry( UdMnemonicCode.UD_Ijb, OpDefs.O_Jz, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso|BitOps.PDef64 ),
            /* 0732 */ new UdItabEntry( UdMnemonicCode.UD_Ijae, OpDefs.O_Jb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0733 */ new UdItabEntry( UdMnemonicCode.UD_Ijae, OpDefs.O_Jz, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso|BitOps.PDef64 ),
            /* 0734 */ new UdItabEntry( UdMnemonicCode.UD_Ijz, OpDefs.O_Jb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0735 */ new UdItabEntry( UdMnemonicCode.UD_Ijz, OpDefs.O_Jz, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso|BitOps.PDef64 ),
            /* 0736 */ new UdItabEntry( UdMnemonicCode.UD_Ijnz, OpDefs.O_Jb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0737 */ new UdItabEntry( UdMnemonicCode.UD_Ijnz, OpDefs.O_Jz, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso|BitOps.PDef64 ),
            /* 0738 */ new UdItabEntry( UdMnemonicCode.UD_Ijbe, OpDefs.O_Jb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0739 */ new UdItabEntry( UdMnemonicCode.UD_Ijbe, OpDefs.O_Jz, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso|BitOps.PDef64 ),
            /* 0740 */ new UdItabEntry( UdMnemonicCode.UD_Ija, OpDefs.O_Jb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0741 */ new UdItabEntry( UdMnemonicCode.UD_Ija, OpDefs.O_Jz, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso|BitOps.PDef64 ),
            /* 0742 */ new UdItabEntry( UdMnemonicCode.UD_Ijs, OpDefs.O_Jb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0743 */ new UdItabEntry( UdMnemonicCode.UD_Ijs, OpDefs.O_Jz, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso|BitOps.PDef64 ),
            /* 0744 */ new UdItabEntry( UdMnemonicCode.UD_Ijns, OpDefs.O_Jb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0745 */ new UdItabEntry( UdMnemonicCode.UD_Ijns, OpDefs.O_Jz, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso|BitOps.PDef64 ),
            /* 0746 */ new UdItabEntry( UdMnemonicCode.UD_Ijp, OpDefs.O_Jb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0747 */ new UdItabEntry( UdMnemonicCode.UD_Ijp, OpDefs.O_Jz, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso|BitOps.PDef64 ),
            /* 0748 */ new UdItabEntry( UdMnemonicCode.UD_Ijnp, OpDefs.O_Jb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0749 */ new UdItabEntry( UdMnemonicCode.UD_Ijnp, OpDefs.O_Jz, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso|BitOps.PDef64 ),
            /* 0750 */ new UdItabEntry( UdMnemonicCode.UD_Ijl, OpDefs.O_Jb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0751 */ new UdItabEntry( UdMnemonicCode.UD_Ijl, OpDefs.O_Jz, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso|BitOps.PDef64 ),
            /* 0752 */ new UdItabEntry( UdMnemonicCode.UD_Ijge, OpDefs.O_Jb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0753 */ new UdItabEntry( UdMnemonicCode.UD_Ijge, OpDefs.O_Jz, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso|BitOps.PDef64 ),
            /* 0754 */ new UdItabEntry( UdMnemonicCode.UD_Ijle, OpDefs.O_Jb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0755 */ new UdItabEntry( UdMnemonicCode.UD_Ijle, OpDefs.O_Jz, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso|BitOps.PDef64 ),
            /* 0756 */ new UdItabEntry( UdMnemonicCode.UD_Ijg, OpDefs.O_Jb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0757 */ new UdItabEntry( UdMnemonicCode.UD_Ijg, OpDefs.O_Jz, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso|BitOps.PDef64 ),
            /* 0758 */ new UdItabEntry( UdMnemonicCode.UD_Ijcxz, OpDefs.O_Jb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso ),
            /* 0759 */ new UdItabEntry( UdMnemonicCode.UD_Ijecxz, OpDefs.O_Jb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso ),
            /* 0760 */ new UdItabEntry( UdMnemonicCode.UD_Ijrcxz, OpDefs.O_Jb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso ),
            /* 0761 */ new UdItabEntry( UdMnemonicCode.UD_Ijmp, OpDefs.O_Ev, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb|BitOps.PDef64 ),
            /* 0762 */ new UdItabEntry( UdMnemonicCode.UD_Ijmp, OpDefs.O_Fv, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0763 */ new UdItabEntry( UdMnemonicCode.UD_Ijmp, OpDefs.O_Jz, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso|BitOps.PDef64 ),
            /* 0764 */ new UdItabEntry( UdMnemonicCode.UD_Ijmp, OpDefs.O_Av, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso ),
            /* 0765 */ new UdItabEntry( UdMnemonicCode.UD_Ijmp, OpDefs.O_Jb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PDef64 ),
            /* 0766 */ new UdItabEntry( UdMnemonicCode.UD_Ilahf, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0767 */ new UdItabEntry( UdMnemonicCode.UD_Ilar, OpDefs.O_Gv, OpDefs.O_Ew, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0768 */ new UdItabEntry( UdMnemonicCode.UD_Ildmxcsr, OpDefs.O_Md, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0769 */ new UdItabEntry( UdMnemonicCode.UD_Ilds, OpDefs.O_Gv, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso ),
            /* 0770 */ new UdItabEntry( UdMnemonicCode.UD_Ilea, OpDefs.O_Gv, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0771 */ new UdItabEntry( UdMnemonicCode.UD_Iles, OpDefs.O_Gv, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso ),
            /* 0772 */ new UdItabEntry( UdMnemonicCode.UD_Ilfs, OpDefs.O_Gz, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0773 */ new UdItabEntry( UdMnemonicCode.UD_Ilgs, OpDefs.O_Gz, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0774 */ new UdItabEntry( UdMnemonicCode.UD_Ilidt, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0775 */ new UdItabEntry( UdMnemonicCode.UD_Ilss, OpDefs.O_Gv, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0776 */ new UdItabEntry( UdMnemonicCode.UD_Ileave, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0777 */ new UdItabEntry( UdMnemonicCode.UD_Ilfence, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0778 */ new UdItabEntry( UdMnemonicCode.UD_Ilfence, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0779 */ new UdItabEntry( UdMnemonicCode.UD_Ilfence, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0780 */ new UdItabEntry( UdMnemonicCode.UD_Ilfence, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0781 */ new UdItabEntry( UdMnemonicCode.UD_Ilfence, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0782 */ new UdItabEntry( UdMnemonicCode.UD_Ilfence, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0783 */ new UdItabEntry( UdMnemonicCode.UD_Ilfence, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0784 */ new UdItabEntry( UdMnemonicCode.UD_Ilfence, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0785 */ new UdItabEntry( UdMnemonicCode.UD_Ilgdt, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0786 */ new UdItabEntry( UdMnemonicCode.UD_Illdt, OpDefs.O_Ew, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0787 */ new UdItabEntry( UdMnemonicCode.UD_Ilmsw, OpDefs.O_Ew, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0788 */ new UdItabEntry( UdMnemonicCode.UD_Ilmsw, OpDefs.O_Ew, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0789 */ new UdItabEntry( UdMnemonicCode.UD_Ilock, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0790 */ new UdItabEntry( UdMnemonicCode.UD_Ilodsb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PStr|BitOps.PSeg ),
            /* 0791 */ new UdItabEntry( UdMnemonicCode.UD_Ilodsw, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PStr|BitOps.PSeg|BitOps.POso|BitOps.PRexw ),
            /* 0792 */ new UdItabEntry( UdMnemonicCode.UD_Ilodsd, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PStr|BitOps.PSeg|BitOps.POso|BitOps.PRexw ),
            /* 0793 */ new UdItabEntry( UdMnemonicCode.UD_Ilodsq, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PStr|BitOps.PSeg|BitOps.POso|BitOps.PRexw ),
            /* 0794 */ new UdItabEntry( UdMnemonicCode.UD_Iloopne, OpDefs.O_Jb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0795 */ new UdItabEntry( UdMnemonicCode.UD_Iloope, OpDefs.O_Jb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0796 */ new UdItabEntry( UdMnemonicCode.UD_Iloop, OpDefs.O_Jb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0797 */ new UdItabEntry( UdMnemonicCode.UD_Ilsl, OpDefs.O_Gv, OpDefs.O_Ew, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0798 */ new UdItabEntry( UdMnemonicCode.UD_Iltr, OpDefs.O_Ew, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0799 */ new UdItabEntry( UdMnemonicCode.UD_Imaskmovq, OpDefs.O_P, OpDefs.O_N, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0800 */ new UdItabEntry( UdMnemonicCode.UD_Imaxpd, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0801 */ new UdItabEntry( UdMnemonicCode.UD_Ivmaxpd, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb|BitOps.PVexl ),
            /* 0802 */ new UdItabEntry( UdMnemonicCode.UD_Imaxps, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0803 */ new UdItabEntry( UdMnemonicCode.UD_Ivmaxps, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb|BitOps.PVexl ),
            /* 0804 */ new UdItabEntry( UdMnemonicCode.UD_Imaxsd, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0805 */ new UdItabEntry( UdMnemonicCode.UD_Ivmaxsd, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0806 */ new UdItabEntry( UdMnemonicCode.UD_Imaxss, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0807 */ new UdItabEntry( UdMnemonicCode.UD_Ivmaxss, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0808 */ new UdItabEntry( UdMnemonicCode.UD_Imfence, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0809 */ new UdItabEntry( UdMnemonicCode.UD_Imfence, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0810 */ new UdItabEntry( UdMnemonicCode.UD_Imfence, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0811 */ new UdItabEntry( UdMnemonicCode.UD_Imfence, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0812 */ new UdItabEntry( UdMnemonicCode.UD_Imfence, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0813 */ new UdItabEntry( UdMnemonicCode.UD_Imfence, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0814 */ new UdItabEntry( UdMnemonicCode.UD_Imfence, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0815 */ new UdItabEntry( UdMnemonicCode.UD_Imfence, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0816 */ new UdItabEntry( UdMnemonicCode.UD_Iminpd, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0817 */ new UdItabEntry( UdMnemonicCode.UD_Ivminpd, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb|BitOps.PVexl ),
            /* 0818 */ new UdItabEntry( UdMnemonicCode.UD_Iminps, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0819 */ new UdItabEntry( UdMnemonicCode.UD_Ivminps, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0820 */ new UdItabEntry( UdMnemonicCode.UD_Iminsd, OpDefs.O_V, OpDefs.O_MqU, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0821 */ new UdItabEntry( UdMnemonicCode.UD_Ivminsd, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_MqU, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0822 */ new UdItabEntry( UdMnemonicCode.UD_Iminss, OpDefs.O_V, OpDefs.O_MdU, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0823 */ new UdItabEntry( UdMnemonicCode.UD_Ivminss, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_MdU, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0824 */ new UdItabEntry( UdMnemonicCode.UD_Imonitor, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0825 */ new UdItabEntry( UdMnemonicCode.UD_Imontmul, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0826 */ new UdItabEntry( UdMnemonicCode.UD_Imov, OpDefs.O_Eb, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0827 */ new UdItabEntry( UdMnemonicCode.UD_Imov, OpDefs.O_Ev, OpDefs.O_sIz, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0828 */ new UdItabEntry( UdMnemonicCode.UD_Imov, OpDefs.O_Eb, OpDefs.O_Gb, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0829 */ new UdItabEntry( UdMnemonicCode.UD_Imov, OpDefs.O_Ev, OpDefs.O_Gv, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0830 */ new UdItabEntry( UdMnemonicCode.UD_Imov, OpDefs.O_Gb, OpDefs.O_Eb, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0831 */ new UdItabEntry( UdMnemonicCode.UD_Imov, OpDefs.O_Gv, OpDefs.O_Ev, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0832 */ new UdItabEntry( UdMnemonicCode.UD_Imov, OpDefs.O_MwRv, OpDefs.O_S, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0833 */ new UdItabEntry( UdMnemonicCode.UD_Imov, OpDefs.O_S, OpDefs.O_MwRv, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0834 */ new UdItabEntry( UdMnemonicCode.UD_Imov, OpDefs.O_AL, OpDefs.O_Ob, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0835 */ new UdItabEntry( UdMnemonicCode.UD_Imov, OpDefs.O_rAX, OpDefs.O_Ov, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw ),
            /* 0836 */ new UdItabEntry( UdMnemonicCode.UD_Imov, OpDefs.O_Ob, OpDefs.O_AL, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0837 */ new UdItabEntry( UdMnemonicCode.UD_Imov, OpDefs.O_Ov, OpDefs.O_rAX, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw ),
            /* 0838 */ new UdItabEntry( UdMnemonicCode.UD_Imov, OpDefs.O_R0b, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PRexb ),
            /* 0839 */ new UdItabEntry( UdMnemonicCode.UD_Imov, OpDefs.O_R1b, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PRexb ),
            /* 0840 */ new UdItabEntry( UdMnemonicCode.UD_Imov, OpDefs.O_R2b, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PRexb ),
            /* 0841 */ new UdItabEntry( UdMnemonicCode.UD_Imov, OpDefs.O_R3b, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PRexb ),
            /* 0842 */ new UdItabEntry( UdMnemonicCode.UD_Imov, OpDefs.O_R4b, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PRexb ),
            /* 0843 */ new UdItabEntry( UdMnemonicCode.UD_Imov, OpDefs.O_R5b, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PRexb ),
            /* 0844 */ new UdItabEntry( UdMnemonicCode.UD_Imov, OpDefs.O_R6b, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PRexb ),
            /* 0845 */ new UdItabEntry( UdMnemonicCode.UD_Imov, OpDefs.O_R7b, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PRexb ),
            /* 0846 */ new UdItabEntry( UdMnemonicCode.UD_Imov, OpDefs.O_R0v, OpDefs.O_Iv, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso|BitOps.PRexw|BitOps.PRexb ),
            /* 0847 */ new UdItabEntry( UdMnemonicCode.UD_Imov, OpDefs.O_R1v, OpDefs.O_Iv, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso|BitOps.PRexw|BitOps.PRexb ),
            /* 0848 */ new UdItabEntry( UdMnemonicCode.UD_Imov, OpDefs.O_R2v, OpDefs.O_Iv, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso|BitOps.PRexw|BitOps.PRexb ),
            /* 0849 */ new UdItabEntry( UdMnemonicCode.UD_Imov, OpDefs.O_R3v, OpDefs.O_Iv, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso|BitOps.PRexw|BitOps.PRexb ),
            /* 0850 */ new UdItabEntry( UdMnemonicCode.UD_Imov, OpDefs.O_R4v, OpDefs.O_Iv, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso|BitOps.PRexw|BitOps.PRexb ),
            /* 0851 */ new UdItabEntry( UdMnemonicCode.UD_Imov, OpDefs.O_R5v, OpDefs.O_Iv, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso|BitOps.PRexw|BitOps.PRexb ),
            /* 0852 */ new UdItabEntry( UdMnemonicCode.UD_Imov, OpDefs.O_R6v, OpDefs.O_Iv, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso|BitOps.PRexw|BitOps.PRexb ),
            /* 0853 */ new UdItabEntry( UdMnemonicCode.UD_Imov, OpDefs.O_R7v, OpDefs.O_Iv, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso|BitOps.PRexw|BitOps.PRexb ),
            /* 0854 */ new UdItabEntry( UdMnemonicCode.UD_Imov, OpDefs.O_R, OpDefs.O_C, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PRexr|BitOps.PRexw|BitOps.PRexb ),
            /* 0855 */ new UdItabEntry( UdMnemonicCode.UD_Imov, OpDefs.O_R, OpDefs.O_D, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PRexr|BitOps.PRexw|BitOps.PRexb ),
            /* 0856 */ new UdItabEntry( UdMnemonicCode.UD_Imov, OpDefs.O_C, OpDefs.O_R, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PRexr|BitOps.PRexw|BitOps.PRexb ),
            /* 0857 */ new UdItabEntry( UdMnemonicCode.UD_Imov, OpDefs.O_D, OpDefs.O_R, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PRexr|BitOps.PRexw|BitOps.PRexb ),
            /* 0858 */ new UdItabEntry( UdMnemonicCode.UD_Imovapd, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0859 */ new UdItabEntry( UdMnemonicCode.UD_Ivmovapd, OpDefs.O_Vx, OpDefs.O_Wx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb|BitOps.PVexl ),
            /* 0860 */ new UdItabEntry( UdMnemonicCode.UD_Imovapd, OpDefs.O_W, OpDefs.O_V, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0861 */ new UdItabEntry( UdMnemonicCode.UD_Ivmovapd, OpDefs.O_Wx, OpDefs.O_Vx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb|BitOps.PVexl ),
            /* 0862 */ new UdItabEntry( UdMnemonicCode.UD_Imovaps, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0863 */ new UdItabEntry( UdMnemonicCode.UD_Ivmovaps, OpDefs.O_Vx, OpDefs.O_Wx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb|BitOps.PVexl ),
            /* 0864 */ new UdItabEntry( UdMnemonicCode.UD_Imovaps, OpDefs.O_W, OpDefs.O_V, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0865 */ new UdItabEntry( UdMnemonicCode.UD_Ivmovaps, OpDefs.O_Wx, OpDefs.O_Vx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb|BitOps.PVexl ),
            /* 0866 */ new UdItabEntry( UdMnemonicCode.UD_Imovd, OpDefs.O_P, OpDefs.O_Ey, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0867 */ new UdItabEntry( UdMnemonicCode.UD_Imovd, OpDefs.O_P, OpDefs.O_Ey, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0868 */ new UdItabEntry( UdMnemonicCode.UD_Imovd, OpDefs.O_V, OpDefs.O_Ey, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0869 */ new UdItabEntry( UdMnemonicCode.UD_Ivmovd, OpDefs.O_Vx, OpDefs.O_Ey, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0870 */ new UdItabEntry( UdMnemonicCode.UD_Imovd, OpDefs.O_V, OpDefs.O_Ey, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0871 */ new UdItabEntry( UdMnemonicCode.UD_Ivmovd, OpDefs.O_Vx, OpDefs.O_Ey, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0872 */ new UdItabEntry( UdMnemonicCode.UD_Imovd, OpDefs.O_Ey, OpDefs.O_P, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0873 */ new UdItabEntry( UdMnemonicCode.UD_Imovd, OpDefs.O_Ey, OpDefs.O_P, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0874 */ new UdItabEntry( UdMnemonicCode.UD_Imovd, OpDefs.O_Ey, OpDefs.O_V, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0875 */ new UdItabEntry( UdMnemonicCode.UD_Ivmovd, OpDefs.O_Ey, OpDefs.O_Vx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0876 */ new UdItabEntry( UdMnemonicCode.UD_Imovd, OpDefs.O_Ey, OpDefs.O_V, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0877 */ new UdItabEntry( UdMnemonicCode.UD_Ivmovd, OpDefs.O_Ey, OpDefs.O_Vx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0878 */ new UdItabEntry( UdMnemonicCode.UD_Imovhpd, OpDefs.O_V, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0879 */ new UdItabEntry( UdMnemonicCode.UD_Ivmovhpd, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_M, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0880 */ new UdItabEntry( UdMnemonicCode.UD_Imovhpd, OpDefs.O_M, OpDefs.O_V, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0881 */ new UdItabEntry( UdMnemonicCode.UD_Ivmovhpd, OpDefs.O_M, OpDefs.O_Vx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0882 */ new UdItabEntry( UdMnemonicCode.UD_Imovhps, OpDefs.O_V, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0883 */ new UdItabEntry( UdMnemonicCode.UD_Ivmovhps, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_M, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0884 */ new UdItabEntry( UdMnemonicCode.UD_Imovhps, OpDefs.O_M, OpDefs.O_V, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0885 */ new UdItabEntry( UdMnemonicCode.UD_Ivmovhps, OpDefs.O_M, OpDefs.O_Vx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0886 */ new UdItabEntry( UdMnemonicCode.UD_Imovlhps, OpDefs.O_V, OpDefs.O_U, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0887 */ new UdItabEntry( UdMnemonicCode.UD_Ivmovlhps, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Ux, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0888 */ new UdItabEntry( UdMnemonicCode.UD_Imovlpd, OpDefs.O_V, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0889 */ new UdItabEntry( UdMnemonicCode.UD_Ivmovlpd, OpDefs.O_Vx, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0890 */ new UdItabEntry( UdMnemonicCode.UD_Imovlpd, OpDefs.O_M, OpDefs.O_V, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0891 */ new UdItabEntry( UdMnemonicCode.UD_Ivmovlpd, OpDefs.O_M, OpDefs.O_Vx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0892 */ new UdItabEntry( UdMnemonicCode.UD_Imovlps, OpDefs.O_V, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0893 */ new UdItabEntry( UdMnemonicCode.UD_Ivmovlps, OpDefs.O_Vx, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0894 */ new UdItabEntry( UdMnemonicCode.UD_Imovlps, OpDefs.O_M, OpDefs.O_V, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0895 */ new UdItabEntry( UdMnemonicCode.UD_Ivmovlps, OpDefs.O_M, OpDefs.O_Vx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0896 */ new UdItabEntry( UdMnemonicCode.UD_Imovhlps, OpDefs.O_V, OpDefs.O_U, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0897 */ new UdItabEntry( UdMnemonicCode.UD_Ivmovhlps, OpDefs.O_Vx, OpDefs.O_Ux, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0898 */ new UdItabEntry( UdMnemonicCode.UD_Imovmskpd, OpDefs.O_Gd, OpDefs.O_U, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso|BitOps.PRexr|BitOps.PRexb ),
            /* 0899 */ new UdItabEntry( UdMnemonicCode.UD_Ivmovmskpd, OpDefs.O_Gd, OpDefs.O_Ux, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso|BitOps.PRexr|BitOps.PRexb|BitOps.PVexl ),
            /* 0900 */ new UdItabEntry( UdMnemonicCode.UD_Imovmskps, OpDefs.O_Gd, OpDefs.O_U, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso|BitOps.PRexr|BitOps.PRexb ),
            /* 0901 */ new UdItabEntry( UdMnemonicCode.UD_Ivmovmskps, OpDefs.O_Gd, OpDefs.O_Ux, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso|BitOps.PRexr|BitOps.PRexb ),
            /* 0902 */ new UdItabEntry( UdMnemonicCode.UD_Imovntdq, OpDefs.O_M, OpDefs.O_V, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0903 */ new UdItabEntry( UdMnemonicCode.UD_Ivmovntdq, OpDefs.O_M, OpDefs.O_Vx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb|BitOps.PVexl ),
            /* 0904 */ new UdItabEntry( UdMnemonicCode.UD_Imovnti, OpDefs.O_M, OpDefs.O_Gy, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0905 */ new UdItabEntry( UdMnemonicCode.UD_Imovntpd, OpDefs.O_M, OpDefs.O_V, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0906 */ new UdItabEntry( UdMnemonicCode.UD_Ivmovntpd, OpDefs.O_M, OpDefs.O_Vx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb|BitOps.PVexl ),
            /* 0907 */ new UdItabEntry( UdMnemonicCode.UD_Imovntps, OpDefs.O_M, OpDefs.O_V, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0908 */ new UdItabEntry( UdMnemonicCode.UD_Ivmovntps, OpDefs.O_M, OpDefs.O_Vx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb|BitOps.PVexl ),
            /* 0909 */ new UdItabEntry( UdMnemonicCode.UD_Imovntq, OpDefs.O_M, OpDefs.O_P, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0910 */ new UdItabEntry( UdMnemonicCode.UD_Imovq, OpDefs.O_P, OpDefs.O_Eq, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0911 */ new UdItabEntry( UdMnemonicCode.UD_Imovq, OpDefs.O_V, OpDefs.O_Eq, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0912 */ new UdItabEntry( UdMnemonicCode.UD_Ivmovq, OpDefs.O_Vx, OpDefs.O_Eq, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0913 */ new UdItabEntry( UdMnemonicCode.UD_Imovq, OpDefs.O_Eq, OpDefs.O_P, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0914 */ new UdItabEntry( UdMnemonicCode.UD_Imovq, OpDefs.O_Eq, OpDefs.O_V, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0915 */ new UdItabEntry( UdMnemonicCode.UD_Ivmovq, OpDefs.O_Eq, OpDefs.O_Vx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0916 */ new UdItabEntry( UdMnemonicCode.UD_Imovq, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0917 */ new UdItabEntry( UdMnemonicCode.UD_Ivmovq, OpDefs.O_Vx, OpDefs.O_Wx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0918 */ new UdItabEntry( UdMnemonicCode.UD_Imovq, OpDefs.O_W, OpDefs.O_V, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0919 */ new UdItabEntry( UdMnemonicCode.UD_Ivmovq, OpDefs.O_Wx, OpDefs.O_Vx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0920 */ new UdItabEntry( UdMnemonicCode.UD_Imovq, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0921 */ new UdItabEntry( UdMnemonicCode.UD_Imovq, OpDefs.O_Q, OpDefs.O_P, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0922 */ new UdItabEntry( UdMnemonicCode.UD_Imovsb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PStr|BitOps.PSeg ),
            /* 0923 */ new UdItabEntry( UdMnemonicCode.UD_Imovsw, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PStr|BitOps.PSeg|BitOps.POso|BitOps.PRexw ),
            /* 0924 */ new UdItabEntry( UdMnemonicCode.UD_Imovsd, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PStr|BitOps.PSeg|BitOps.POso|BitOps.PRexw ),
            /* 0925 */ new UdItabEntry( UdMnemonicCode.UD_Imovsd, OpDefs.O_V, OpDefs.O_MqU, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0926 */ new UdItabEntry( UdMnemonicCode.UD_Imovsd, OpDefs.O_W, OpDefs.O_V, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0927 */ new UdItabEntry( UdMnemonicCode.UD_Imovsq, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PStr|BitOps.PSeg|BitOps.POso|BitOps.PRexw ),
            /* 0928 */ new UdItabEntry( UdMnemonicCode.UD_Imovss, OpDefs.O_V, OpDefs.O_MdU, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0929 */ new UdItabEntry( UdMnemonicCode.UD_Imovss, OpDefs.O_W, OpDefs.O_V, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0930 */ new UdItabEntry( UdMnemonicCode.UD_Imovsx, OpDefs.O_Gv, OpDefs.O_Eb, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0931 */ new UdItabEntry( UdMnemonicCode.UD_Imovsx, OpDefs.O_Gy, OpDefs.O_Ew, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0932 */ new UdItabEntry( UdMnemonicCode.UD_Imovupd, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0933 */ new UdItabEntry( UdMnemonicCode.UD_Ivmovupd, OpDefs.O_Vx, OpDefs.O_Wx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb|BitOps.PVexl ),
            /* 0934 */ new UdItabEntry( UdMnemonicCode.UD_Imovupd, OpDefs.O_W, OpDefs.O_V, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0935 */ new UdItabEntry( UdMnemonicCode.UD_Ivmovupd, OpDefs.O_Wx, OpDefs.O_Vx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb|BitOps.PVexl ),
            /* 0936 */ new UdItabEntry( UdMnemonicCode.UD_Imovups, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0937 */ new UdItabEntry( UdMnemonicCode.UD_Ivmovups, OpDefs.O_Vx, OpDefs.O_Wx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb|BitOps.PVexl ),
            /* 0938 */ new UdItabEntry( UdMnemonicCode.UD_Imovups, OpDefs.O_W, OpDefs.O_V, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0939 */ new UdItabEntry( UdMnemonicCode.UD_Ivmovups, OpDefs.O_Wx, OpDefs.O_Vx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb|BitOps.PVexl ),
            /* 0940 */ new UdItabEntry( UdMnemonicCode.UD_Imovzx, OpDefs.O_Gv, OpDefs.O_Eb, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0941 */ new UdItabEntry( UdMnemonicCode.UD_Imovzx, OpDefs.O_Gy, OpDefs.O_Ew, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0942 */ new UdItabEntry( UdMnemonicCode.UD_Imul, OpDefs.O_Eb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0943 */ new UdItabEntry( UdMnemonicCode.UD_Imul, OpDefs.O_Ev, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0944 */ new UdItabEntry( UdMnemonicCode.UD_Imulpd, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0945 */ new UdItabEntry( UdMnemonicCode.UD_Ivmulpd, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb|BitOps.PVexl ),
            /* 0946 */ new UdItabEntry( UdMnemonicCode.UD_Imulps, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0947 */ new UdItabEntry( UdMnemonicCode.UD_Ivmulps, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb|BitOps.PVexl ),
            /* 0948 */ new UdItabEntry( UdMnemonicCode.UD_Imulsd, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0949 */ new UdItabEntry( UdMnemonicCode.UD_Ivmulsd, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0950 */ new UdItabEntry( UdMnemonicCode.UD_Imulss, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0951 */ new UdItabEntry( UdMnemonicCode.UD_Ivmulss, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0952 */ new UdItabEntry( UdMnemonicCode.UD_Imwait, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0953 */ new UdItabEntry( UdMnemonicCode.UD_Ineg, OpDefs.O_Eb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0954 */ new UdItabEntry( UdMnemonicCode.UD_Ineg, OpDefs.O_Ev, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0955 */ new UdItabEntry( UdMnemonicCode.UD_Inop, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0956 */ new UdItabEntry( UdMnemonicCode.UD_Inop, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0957 */ new UdItabEntry( UdMnemonicCode.UD_Inop, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0958 */ new UdItabEntry( UdMnemonicCode.UD_Inop, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0959 */ new UdItabEntry( UdMnemonicCode.UD_Inop, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0960 */ new UdItabEntry( UdMnemonicCode.UD_Inop, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0961 */ new UdItabEntry( UdMnemonicCode.UD_Inop, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0962 */ new UdItabEntry( UdMnemonicCode.UD_Inot, OpDefs.O_Eb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0963 */ new UdItabEntry( UdMnemonicCode.UD_Inot, OpDefs.O_Ev, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0964 */ new UdItabEntry( UdMnemonicCode.UD_Ior, OpDefs.O_Eb, OpDefs.O_Gb, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0965 */ new UdItabEntry( UdMnemonicCode.UD_Ior, OpDefs.O_Ev, OpDefs.O_Gv, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0966 */ new UdItabEntry( UdMnemonicCode.UD_Ior, OpDefs.O_Gb, OpDefs.O_Eb, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0967 */ new UdItabEntry( UdMnemonicCode.UD_Ior, OpDefs.O_Gv, OpDefs.O_Ev, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0968 */ new UdItabEntry( UdMnemonicCode.UD_Ior, OpDefs.O_AL, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0969 */ new UdItabEntry( UdMnemonicCode.UD_Ior, OpDefs.O_rAX, OpDefs.O_sIz, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso|BitOps.PRexw ),
            /* 0970 */ new UdItabEntry( UdMnemonicCode.UD_Ior, OpDefs.O_Eb, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0971 */ new UdItabEntry( UdMnemonicCode.UD_Ior, OpDefs.O_Ev, OpDefs.O_sIz, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0972 */ new UdItabEntry( UdMnemonicCode.UD_Ior, OpDefs.O_Eb, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0973 */ new UdItabEntry( UdMnemonicCode.UD_Ior, OpDefs.O_Ev, OpDefs.O_sIb, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0974 */ new UdItabEntry( UdMnemonicCode.UD_Iorpd, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0975 */ new UdItabEntry( UdMnemonicCode.UD_Ivorpd, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb|BitOps.PVexl ),
            /* 0976 */ new UdItabEntry( UdMnemonicCode.UD_Iorps, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0977 */ new UdItabEntry( UdMnemonicCode.UD_Ivorps, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb|BitOps.PVexl ),
            /* 0978 */ new UdItabEntry( UdMnemonicCode.UD_Iout, OpDefs.O_Ib, OpDefs.O_AL, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0979 */ new UdItabEntry( UdMnemonicCode.UD_Iout, OpDefs.O_Ib, OpDefs.O_eAX, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso ),
            /* 0980 */ new UdItabEntry( UdMnemonicCode.UD_Iout, OpDefs.O_DX, OpDefs.O_AL, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 0981 */ new UdItabEntry( UdMnemonicCode.UD_Iout, OpDefs.O_DX, OpDefs.O_eAX, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso ),
            /* 0982 */ new UdItabEntry( UdMnemonicCode.UD_Ioutsb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PStr|BitOps.PSeg ),
            /* 0983 */ new UdItabEntry( UdMnemonicCode.UD_Ioutsw, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PStr|BitOps.POso|BitOps.PSeg ),
            /* 0984 */ new UdItabEntry( UdMnemonicCode.UD_Ioutsd, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PStr|BitOps.POso|BitOps.PSeg ),
            /* 0985 */ new UdItabEntry( UdMnemonicCode.UD_Ipacksswb, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0986 */ new UdItabEntry( UdMnemonicCode.UD_Ivpacksswb, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb|BitOps.PVexl ),
            /* 0987 */ new UdItabEntry( UdMnemonicCode.UD_Ipacksswb, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0988 */ new UdItabEntry( UdMnemonicCode.UD_Ipackssdw, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0989 */ new UdItabEntry( UdMnemonicCode.UD_Ivpackssdw, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb|BitOps.PVexl ),
            /* 0990 */ new UdItabEntry( UdMnemonicCode.UD_Ipackssdw, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0991 */ new UdItabEntry( UdMnemonicCode.UD_Ipackuswb, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0992 */ new UdItabEntry( UdMnemonicCode.UD_Ivpackuswb, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb|BitOps.PVexl ),
            /* 0993 */ new UdItabEntry( UdMnemonicCode.UD_Ipackuswb, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0994 */ new UdItabEntry( UdMnemonicCode.UD_Ipaddb, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0995 */ new UdItabEntry( UdMnemonicCode.UD_Ivpaddb, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb|BitOps.PVexl ),
            /* 0996 */ new UdItabEntry( UdMnemonicCode.UD_Ipaddb, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0997 */ new UdItabEntry( UdMnemonicCode.UD_Ipaddw, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0998 */ new UdItabEntry( UdMnemonicCode.UD_Ipaddw, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 0999 */ new UdItabEntry( UdMnemonicCode.UD_Ivpaddw, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb|BitOps.PVexl ),
            /* 1000 */ new UdItabEntry( UdMnemonicCode.UD_Ipaddd, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1001 */ new UdItabEntry( UdMnemonicCode.UD_Ipaddd, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1002 */ new UdItabEntry( UdMnemonicCode.UD_Ivpaddd, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb|BitOps.PVexl ),
            /* 1003 */ new UdItabEntry( UdMnemonicCode.UD_Ipaddsb, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1004 */ new UdItabEntry( UdMnemonicCode.UD_Ipaddsb, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1005 */ new UdItabEntry( UdMnemonicCode.UD_Ivpaddsb, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1006 */ new UdItabEntry( UdMnemonicCode.UD_Ipaddsw, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1007 */ new UdItabEntry( UdMnemonicCode.UD_Ipaddsw, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1008 */ new UdItabEntry( UdMnemonicCode.UD_Ivpaddsw, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1009 */ new UdItabEntry( UdMnemonicCode.UD_Ipaddusb, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1010 */ new UdItabEntry( UdMnemonicCode.UD_Ipaddusb, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1011 */ new UdItabEntry( UdMnemonicCode.UD_Ivpaddusb, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1012 */ new UdItabEntry( UdMnemonicCode.UD_Ipaddusw, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1013 */ new UdItabEntry( UdMnemonicCode.UD_Ipaddusw, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1014 */ new UdItabEntry( UdMnemonicCode.UD_Ivpaddusw, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1015 */ new UdItabEntry( UdMnemonicCode.UD_Ipand, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1016 */ new UdItabEntry( UdMnemonicCode.UD_Ivpand, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1017 */ new UdItabEntry( UdMnemonicCode.UD_Ipand, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1018 */ new UdItabEntry( UdMnemonicCode.UD_Ipandn, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1019 */ new UdItabEntry( UdMnemonicCode.UD_Ivpandn, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1020 */ new UdItabEntry( UdMnemonicCode.UD_Ipandn, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1021 */ new UdItabEntry( UdMnemonicCode.UD_Ipavgb, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1022 */ new UdItabEntry( UdMnemonicCode.UD_Ivpavgb, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1023 */ new UdItabEntry( UdMnemonicCode.UD_Ipavgb, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1024 */ new UdItabEntry( UdMnemonicCode.UD_Ipavgw, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1025 */ new UdItabEntry( UdMnemonicCode.UD_Ivpavgw, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1026 */ new UdItabEntry( UdMnemonicCode.UD_Ipavgw, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1027 */ new UdItabEntry( UdMnemonicCode.UD_Ipcmpeqb, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1028 */ new UdItabEntry( UdMnemonicCode.UD_Ipcmpeqb, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1029 */ new UdItabEntry( UdMnemonicCode.UD_Ivpcmpeqb, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1030 */ new UdItabEntry( UdMnemonicCode.UD_Ipcmpeqw, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1031 */ new UdItabEntry( UdMnemonicCode.UD_Ipcmpeqw, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1032 */ new UdItabEntry( UdMnemonicCode.UD_Ivpcmpeqw, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1033 */ new UdItabEntry( UdMnemonicCode.UD_Ipcmpeqd, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1034 */ new UdItabEntry( UdMnemonicCode.UD_Ipcmpeqd, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1035 */ new UdItabEntry( UdMnemonicCode.UD_Ivpcmpeqd, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1036 */ new UdItabEntry( UdMnemonicCode.UD_Ipcmpgtb, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1037 */ new UdItabEntry( UdMnemonicCode.UD_Ivpcmpgtb, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1038 */ new UdItabEntry( UdMnemonicCode.UD_Ipcmpgtb, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1039 */ new UdItabEntry( UdMnemonicCode.UD_Ipcmpgtw, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1040 */ new UdItabEntry( UdMnemonicCode.UD_Ivpcmpgtw, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1041 */ new UdItabEntry( UdMnemonicCode.UD_Ipcmpgtw, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1042 */ new UdItabEntry( UdMnemonicCode.UD_Ipcmpgtd, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1043 */ new UdItabEntry( UdMnemonicCode.UD_Ivpcmpgtd, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1044 */ new UdItabEntry( UdMnemonicCode.UD_Ipcmpgtd, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1045 */ new UdItabEntry( UdMnemonicCode.UD_Ipextrb, OpDefs.O_MbRv, OpDefs.O_V, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexx|BitOps.PRexr|BitOps.PRexb|BitOps.PDef64 ),
            /* 1046 */ new UdItabEntry( UdMnemonicCode.UD_Ivpextrb, OpDefs.O_MbRv, OpDefs.O_Vx, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexx|BitOps.PRexr|BitOps.PRexb|BitOps.PDef64 ),
            /* 1047 */ new UdItabEntry( UdMnemonicCode.UD_Ipextrd, OpDefs.O_Ed, OpDefs.O_V, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexw|BitOps.PRexb ),
            /* 1048 */ new UdItabEntry( UdMnemonicCode.UD_Ivpextrd, OpDefs.O_Ed, OpDefs.O_Vx, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexw|BitOps.PRexb ),
            /* 1049 */ new UdItabEntry( UdMnemonicCode.UD_Ipextrd, OpDefs.O_Ed, OpDefs.O_V, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexw|BitOps.PRexb ),
            /* 1050 */ new UdItabEntry( UdMnemonicCode.UD_Ivpextrd, OpDefs.O_Ed, OpDefs.O_Vx, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexw|BitOps.PRexb ),
            /* 1051 */ new UdItabEntry( UdMnemonicCode.UD_Ipextrq, OpDefs.O_Eq, OpDefs.O_V, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexw|BitOps.PRexb|BitOps.PDef64 ),
            /* 1052 */ new UdItabEntry( UdMnemonicCode.UD_Ivpextrq, OpDefs.O_Eq, OpDefs.O_Vx, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexw|BitOps.PRexb|BitOps.PDef64 ),
            /* 1053 */ new UdItabEntry( UdMnemonicCode.UD_Ipextrw, OpDefs.O_Gd, OpDefs.O_U, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexb ),
            /* 1054 */ new UdItabEntry( UdMnemonicCode.UD_Ivpextrw, OpDefs.O_Gd, OpDefs.O_Ux, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexb ),
            /* 1055 */ new UdItabEntry( UdMnemonicCode.UD_Ipextrw, OpDefs.O_Gd, OpDefs.O_N, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1056 */ new UdItabEntry( UdMnemonicCode.UD_Ipextrw, OpDefs.O_MwRd, OpDefs.O_V, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexx|BitOps.PRexr|BitOps.PRexb ),
            /* 1057 */ new UdItabEntry( UdMnemonicCode.UD_Ivpextrw, OpDefs.O_MwRd, OpDefs.O_Vx, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexx|BitOps.PRexr|BitOps.PRexb ),
            /* 1058 */ new UdItabEntry( UdMnemonicCode.UD_Ipinsrb, OpDefs.O_V, OpDefs.O_MbRd, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1059 */ new UdItabEntry( UdMnemonicCode.UD_Ipinsrw, OpDefs.O_P, OpDefs.O_MwRy, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb|BitOps.PDef64 ),
            /* 1060 */ new UdItabEntry( UdMnemonicCode.UD_Ipinsrw, OpDefs.O_V, OpDefs.O_MwRy, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb|BitOps.PDef64 ),
            /* 1061 */ new UdItabEntry( UdMnemonicCode.UD_Ivpinsrw, OpDefs.O_Vx, OpDefs.O_MwRy, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb|BitOps.PDef64 ),
            /* 1062 */ new UdItabEntry( UdMnemonicCode.UD_Ipinsrd, OpDefs.O_V, OpDefs.O_Ed, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1063 */ new UdItabEntry( UdMnemonicCode.UD_Ipinsrd, OpDefs.O_V, OpDefs.O_Ed, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1064 */ new UdItabEntry( UdMnemonicCode.UD_Ipinsrq, OpDefs.O_V, OpDefs.O_Eq, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1065 */ new UdItabEntry( UdMnemonicCode.UD_Ivpinsrb, OpDefs.O_V, OpDefs.O_H, OpDefs.O_MbRd, OpDefs.O_Ib, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1066 */ new UdItabEntry( UdMnemonicCode.UD_Ivpinsrd, OpDefs.O_V, OpDefs.O_H, OpDefs.O_Ed, OpDefs.O_Ib, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1067 */ new UdItabEntry( UdMnemonicCode.UD_Ivpinsrd, OpDefs.O_V, OpDefs.O_H, OpDefs.O_Ed, OpDefs.O_Ib, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1068 */ new UdItabEntry( UdMnemonicCode.UD_Ivpinsrq, OpDefs.O_V, OpDefs.O_H, OpDefs.O_Eq, OpDefs.O_Ib, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1069 */ new UdItabEntry( UdMnemonicCode.UD_Ipmaddwd, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1070 */ new UdItabEntry( UdMnemonicCode.UD_Ipmaddwd, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1071 */ new UdItabEntry( UdMnemonicCode.UD_Ivpmaddwd, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1072 */ new UdItabEntry( UdMnemonicCode.UD_Ipmaxsw, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1073 */ new UdItabEntry( UdMnemonicCode.UD_Ivpmaxsw, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1074 */ new UdItabEntry( UdMnemonicCode.UD_Ipmaxsw, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1075 */ new UdItabEntry( UdMnemonicCode.UD_Ipmaxub, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1076 */ new UdItabEntry( UdMnemonicCode.UD_Ipmaxub, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1077 */ new UdItabEntry( UdMnemonicCode.UD_Ivpmaxub, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1078 */ new UdItabEntry( UdMnemonicCode.UD_Ipminsw, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1079 */ new UdItabEntry( UdMnemonicCode.UD_Ivpminsw, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1080 */ new UdItabEntry( UdMnemonicCode.UD_Ipminsw, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1081 */ new UdItabEntry( UdMnemonicCode.UD_Ipminub, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1082 */ new UdItabEntry( UdMnemonicCode.UD_Ivpminub, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1083 */ new UdItabEntry( UdMnemonicCode.UD_Ipminub, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1084 */ new UdItabEntry( UdMnemonicCode.UD_Ipmovmskb, OpDefs.O_Gd, OpDefs.O_U, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso|BitOps.PRexr|BitOps.PRexw|BitOps.PRexb ),
            /* 1085 */ new UdItabEntry( UdMnemonicCode.UD_Ivpmovmskb, OpDefs.O_Gd, OpDefs.O_Ux, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso|BitOps.PRexr|BitOps.PRexw|BitOps.PRexb ),
            /* 1086 */ new UdItabEntry( UdMnemonicCode.UD_Ipmovmskb, OpDefs.O_Gd, OpDefs.O_N, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso|BitOps.PRexr|BitOps.PRexw|BitOps.PRexb ),
            /* 1087 */ new UdItabEntry( UdMnemonicCode.UD_Ipmulhuw, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1088 */ new UdItabEntry( UdMnemonicCode.UD_Ipmulhuw, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1089 */ new UdItabEntry( UdMnemonicCode.UD_Ivpmulhuw, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1090 */ new UdItabEntry( UdMnemonicCode.UD_Ipmulhw, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1091 */ new UdItabEntry( UdMnemonicCode.UD_Ivpmulhw, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1092 */ new UdItabEntry( UdMnemonicCode.UD_Ipmulhw, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1093 */ new UdItabEntry( UdMnemonicCode.UD_Ipmullw, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1094 */ new UdItabEntry( UdMnemonicCode.UD_Ipmullw, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1095 */ new UdItabEntry( UdMnemonicCode.UD_Ivpmullw, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1096 */ new UdItabEntry( UdMnemonicCode.UD_Ipop, OpDefs.O_ES, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PInv64 ),
            /* 1097 */ new UdItabEntry( UdMnemonicCode.UD_Ipop, OpDefs.O_SS, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PInv64 ),
            /* 1098 */ new UdItabEntry( UdMnemonicCode.UD_Ipop, OpDefs.O_DS, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PInv64 ),
            /* 1099 */ new UdItabEntry( UdMnemonicCode.UD_Ipop, OpDefs.O_GS, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 1100 */ new UdItabEntry( UdMnemonicCode.UD_Ipop, OpDefs.O_FS, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 1101 */ new UdItabEntry( UdMnemonicCode.UD_Ipop, OpDefs.O_R0v, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso|BitOps.PRexb|BitOps.PDef64 ),
            /* 1102 */ new UdItabEntry( UdMnemonicCode.UD_Ipop, OpDefs.O_R1v, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso|BitOps.PRexb|BitOps.PDef64 ),
            /* 1103 */ new UdItabEntry( UdMnemonicCode.UD_Ipop, OpDefs.O_R2v, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso|BitOps.PRexb|BitOps.PDef64 ),
            /* 1104 */ new UdItabEntry( UdMnemonicCode.UD_Ipop, OpDefs.O_R3v, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso|BitOps.PRexb|BitOps.PDef64 ),
            /* 1105 */ new UdItabEntry( UdMnemonicCode.UD_Ipop, OpDefs.O_R4v, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso|BitOps.PRexb|BitOps.PDef64 ),
            /* 1106 */ new UdItabEntry( UdMnemonicCode.UD_Ipop, OpDefs.O_R5v, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso|BitOps.PRexb|BitOps.PDef64 ),
            /* 1107 */ new UdItabEntry( UdMnemonicCode.UD_Ipop, OpDefs.O_R6v, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso|BitOps.PRexb|BitOps.PDef64 ),
            /* 1108 */ new UdItabEntry( UdMnemonicCode.UD_Ipop, OpDefs.O_R7v, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso|BitOps.PRexb|BitOps.PDef64 ),
            /* 1109 */ new UdItabEntry( UdMnemonicCode.UD_Ipop, OpDefs.O_Ev, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb|BitOps.PDef64 ),
            /* 1110 */ new UdItabEntry( UdMnemonicCode.UD_Ipopa, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso|BitOps.PInv64 ),
            /* 1111 */ new UdItabEntry( UdMnemonicCode.UD_Ipopad, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso|BitOps.PInv64 ),
            /* 1112 */ new UdItabEntry( UdMnemonicCode.UD_Ipopfw, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso ),
            /* 1113 */ new UdItabEntry( UdMnemonicCode.UD_Ipopfd, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso ),
            /* 1114 */ new UdItabEntry( UdMnemonicCode.UD_Ipopfq, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso|BitOps.PDef64 ),
            /* 1115 */ new UdItabEntry( UdMnemonicCode.UD_Ipopfq, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso|BitOps.PDef64 ),
            /* 1116 */ new UdItabEntry( UdMnemonicCode.UD_Ipor, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1117 */ new UdItabEntry( UdMnemonicCode.UD_Ivpor, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1118 */ new UdItabEntry( UdMnemonicCode.UD_Ipor, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1119 */ new UdItabEntry( UdMnemonicCode.UD_Iprefetch, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1120 */ new UdItabEntry( UdMnemonicCode.UD_Iprefetch, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1121 */ new UdItabEntry( UdMnemonicCode.UD_Iprefetch, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1122 */ new UdItabEntry( UdMnemonicCode.UD_Iprefetch, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1123 */ new UdItabEntry( UdMnemonicCode.UD_Iprefetch, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1124 */ new UdItabEntry( UdMnemonicCode.UD_Iprefetch, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1125 */ new UdItabEntry( UdMnemonicCode.UD_Iprefetch, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1126 */ new UdItabEntry( UdMnemonicCode.UD_Iprefetch, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1127 */ new UdItabEntry( UdMnemonicCode.UD_Iprefetchnta, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1128 */ new UdItabEntry( UdMnemonicCode.UD_Iprefetcht0, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1129 */ new UdItabEntry( UdMnemonicCode.UD_Iprefetcht1, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1130 */ new UdItabEntry( UdMnemonicCode.UD_Iprefetcht2, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1131 */ new UdItabEntry( UdMnemonicCode.UD_Ipsadbw, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1132 */ new UdItabEntry( UdMnemonicCode.UD_Ivpsadbw, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1133 */ new UdItabEntry( UdMnemonicCode.UD_Ipsadbw, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1134 */ new UdItabEntry( UdMnemonicCode.UD_Ipshufw, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1135 */ new UdItabEntry( UdMnemonicCode.UD_Ipsllw, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1136 */ new UdItabEntry( UdMnemonicCode.UD_Ipsllw, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1137 */ new UdItabEntry( UdMnemonicCode.UD_Ipsllw, OpDefs.O_U, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PRexb ),
            /* 1138 */ new UdItabEntry( UdMnemonicCode.UD_Ipsllw, OpDefs.O_N, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 1139 */ new UdItabEntry( UdMnemonicCode.UD_Ipslld, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1140 */ new UdItabEntry( UdMnemonicCode.UD_Ipslld, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1141 */ new UdItabEntry( UdMnemonicCode.UD_Ipslld, OpDefs.O_U, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PRexb ),
            /* 1142 */ new UdItabEntry( UdMnemonicCode.UD_Ipslld, OpDefs.O_N, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 1143 */ new UdItabEntry( UdMnemonicCode.UD_Ipsllq, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1144 */ new UdItabEntry( UdMnemonicCode.UD_Ipsllq, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1145 */ new UdItabEntry( UdMnemonicCode.UD_Ipsllq, OpDefs.O_U, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PRexb ),
            /* 1146 */ new UdItabEntry( UdMnemonicCode.UD_Ipsllq, OpDefs.O_N, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 1147 */ new UdItabEntry( UdMnemonicCode.UD_Ipsraw, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1148 */ new UdItabEntry( UdMnemonicCode.UD_Ipsraw, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1149 */ new UdItabEntry( UdMnemonicCode.UD_Ivpsraw, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1150 */ new UdItabEntry( UdMnemonicCode.UD_Ipsraw, OpDefs.O_U, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PRexb ),
            /* 1151 */ new UdItabEntry( UdMnemonicCode.UD_Ivpsraw, OpDefs.O_Hx, OpDefs.O_Ux, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.PRexb ),
            /* 1152 */ new UdItabEntry( UdMnemonicCode.UD_Ipsraw, OpDefs.O_N, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 1153 */ new UdItabEntry( UdMnemonicCode.UD_Ipsrad, OpDefs.O_N, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 1154 */ new UdItabEntry( UdMnemonicCode.UD_Ipsrad, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1155 */ new UdItabEntry( UdMnemonicCode.UD_Ivpsrad, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1156 */ new UdItabEntry( UdMnemonicCode.UD_Ipsrad, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1157 */ new UdItabEntry( UdMnemonicCode.UD_Ipsrad, OpDefs.O_U, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PRexb ),
            /* 1158 */ new UdItabEntry( UdMnemonicCode.UD_Ivpsrad, OpDefs.O_Hx, OpDefs.O_Ux, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.PRexb ),
            /* 1159 */ new UdItabEntry( UdMnemonicCode.UD_Ipsrlw, OpDefs.O_N, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 1160 */ new UdItabEntry( UdMnemonicCode.UD_Ipsrlw, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1161 */ new UdItabEntry( UdMnemonicCode.UD_Ipsrlw, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1162 */ new UdItabEntry( UdMnemonicCode.UD_Ivpsrlw, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1163 */ new UdItabEntry( UdMnemonicCode.UD_Ipsrlw, OpDefs.O_U, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PRexb ),
            /* 1164 */ new UdItabEntry( UdMnemonicCode.UD_Ivpsrlw, OpDefs.O_Hx, OpDefs.O_Ux, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.PRexb ),
            /* 1165 */ new UdItabEntry( UdMnemonicCode.UD_Ipsrld, OpDefs.O_N, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 1166 */ new UdItabEntry( UdMnemonicCode.UD_Ipsrld, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1167 */ new UdItabEntry( UdMnemonicCode.UD_Ipsrld, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1168 */ new UdItabEntry( UdMnemonicCode.UD_Ivpsrld, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1169 */ new UdItabEntry( UdMnemonicCode.UD_Ipsrld, OpDefs.O_U, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PRexb ),
            /* 1170 */ new UdItabEntry( UdMnemonicCode.UD_Ivpsrld, OpDefs.O_Hx, OpDefs.O_Ux, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.PRexb ),
            /* 1171 */ new UdItabEntry( UdMnemonicCode.UD_Ipsrlq, OpDefs.O_N, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 1172 */ new UdItabEntry( UdMnemonicCode.UD_Ipsrlq, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1173 */ new UdItabEntry( UdMnemonicCode.UD_Ipsrlq, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1174 */ new UdItabEntry( UdMnemonicCode.UD_Ivpsrlq, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1175 */ new UdItabEntry( UdMnemonicCode.UD_Ipsrlq, OpDefs.O_U, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PRexb ),
            /* 1176 */ new UdItabEntry( UdMnemonicCode.UD_Ivpsrlq, OpDefs.O_Hx, OpDefs.O_Ux, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.PRexb ),
            /* 1177 */ new UdItabEntry( UdMnemonicCode.UD_Ipsubb, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1178 */ new UdItabEntry( UdMnemonicCode.UD_Ivpsubb, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1179 */ new UdItabEntry( UdMnemonicCode.UD_Ipsubb, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1180 */ new UdItabEntry( UdMnemonicCode.UD_Ipsubw, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1181 */ new UdItabEntry( UdMnemonicCode.UD_Ivpsubw, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1182 */ new UdItabEntry( UdMnemonicCode.UD_Ipsubw, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1183 */ new UdItabEntry( UdMnemonicCode.UD_Ipsubd, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1184 */ new UdItabEntry( UdMnemonicCode.UD_Ipsubd, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1185 */ new UdItabEntry( UdMnemonicCode.UD_Ivpsubd, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1186 */ new UdItabEntry( UdMnemonicCode.UD_Ipsubsb, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1187 */ new UdItabEntry( UdMnemonicCode.UD_Ipsubsb, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1188 */ new UdItabEntry( UdMnemonicCode.UD_Ivpsubsb, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1189 */ new UdItabEntry( UdMnemonicCode.UD_Ipsubsw, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1190 */ new UdItabEntry( UdMnemonicCode.UD_Ipsubsw, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1191 */ new UdItabEntry( UdMnemonicCode.UD_Ivpsubsw, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1192 */ new UdItabEntry( UdMnemonicCode.UD_Ipsubusb, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1193 */ new UdItabEntry( UdMnemonicCode.UD_Ipsubusb, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1194 */ new UdItabEntry( UdMnemonicCode.UD_Ivpsubusb, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1195 */ new UdItabEntry( UdMnemonicCode.UD_Ipsubusw, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1196 */ new UdItabEntry( UdMnemonicCode.UD_Ipsubusw, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1197 */ new UdItabEntry( UdMnemonicCode.UD_Ivpsubusw, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1198 */ new UdItabEntry( UdMnemonicCode.UD_Ipunpckhbw, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1199 */ new UdItabEntry( UdMnemonicCode.UD_Ivpunpckhbw, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1200 */ new UdItabEntry( UdMnemonicCode.UD_Ipunpckhbw, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1201 */ new UdItabEntry( UdMnemonicCode.UD_Ipunpckhwd, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1202 */ new UdItabEntry( UdMnemonicCode.UD_Ivpunpckhwd, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1203 */ new UdItabEntry( UdMnemonicCode.UD_Ipunpckhwd, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1204 */ new UdItabEntry( UdMnemonicCode.UD_Ipunpckhdq, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1205 */ new UdItabEntry( UdMnemonicCode.UD_Ivpunpckhdq, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1206 */ new UdItabEntry( UdMnemonicCode.UD_Ipunpckhdq, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1207 */ new UdItabEntry( UdMnemonicCode.UD_Ipunpcklbw, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1208 */ new UdItabEntry( UdMnemonicCode.UD_Ivpunpcklbw, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1209 */ new UdItabEntry( UdMnemonicCode.UD_Ipunpcklbw, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1210 */ new UdItabEntry( UdMnemonicCode.UD_Ipunpcklwd, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1211 */ new UdItabEntry( UdMnemonicCode.UD_Ivpunpcklwd, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1212 */ new UdItabEntry( UdMnemonicCode.UD_Ipunpcklwd, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1213 */ new UdItabEntry( UdMnemonicCode.UD_Ipunpckldq, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1214 */ new UdItabEntry( UdMnemonicCode.UD_Ivpunpckldq, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1215 */ new UdItabEntry( UdMnemonicCode.UD_Ipunpckldq, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1216 */ new UdItabEntry( UdMnemonicCode.UD_Ipi2fw, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1217 */ new UdItabEntry( UdMnemonicCode.UD_Ipi2fd, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1218 */ new UdItabEntry( UdMnemonicCode.UD_Ipf2iw, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1219 */ new UdItabEntry( UdMnemonicCode.UD_Ipf2id, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1220 */ new UdItabEntry( UdMnemonicCode.UD_Ipfnacc, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1221 */ new UdItabEntry( UdMnemonicCode.UD_Ipfpnacc, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1222 */ new UdItabEntry( UdMnemonicCode.UD_Ipfcmpge, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1223 */ new UdItabEntry( UdMnemonicCode.UD_Ipfmin, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1224 */ new UdItabEntry( UdMnemonicCode.UD_Ipfrcp, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1225 */ new UdItabEntry( UdMnemonicCode.UD_Ipfrsqrt, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1226 */ new UdItabEntry( UdMnemonicCode.UD_Ipfsub, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1227 */ new UdItabEntry( UdMnemonicCode.UD_Ipfadd, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1228 */ new UdItabEntry( UdMnemonicCode.UD_Ipfcmpgt, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1229 */ new UdItabEntry( UdMnemonicCode.UD_Ipfmax, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1230 */ new UdItabEntry( UdMnemonicCode.UD_Ipfrcpit1, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1231 */ new UdItabEntry( UdMnemonicCode.UD_Ipfrsqit1, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1232 */ new UdItabEntry( UdMnemonicCode.UD_Ipfsubr, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1233 */ new UdItabEntry( UdMnemonicCode.UD_Ipfacc, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1234 */ new UdItabEntry( UdMnemonicCode.UD_Ipfcmpeq, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1235 */ new UdItabEntry( UdMnemonicCode.UD_Ipfmul, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1236 */ new UdItabEntry( UdMnemonicCode.UD_Ipfrcpit2, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1237 */ new UdItabEntry( UdMnemonicCode.UD_Ipmulhrw, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1238 */ new UdItabEntry( UdMnemonicCode.UD_Ipswapd, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1239 */ new UdItabEntry( UdMnemonicCode.UD_Ipavgusb, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1240 */ new UdItabEntry( UdMnemonicCode.UD_Ipush, OpDefs.O_ES, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PInv64 ),
            /* 1241 */ new UdItabEntry( UdMnemonicCode.UD_Ipush, OpDefs.O_CS, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PInv64 ),
            /* 1242 */ new UdItabEntry( UdMnemonicCode.UD_Ipush, OpDefs.O_SS, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PInv64 ),
            /* 1243 */ new UdItabEntry( UdMnemonicCode.UD_Ipush, OpDefs.O_DS, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PInv64 ),
            /* 1244 */ new UdItabEntry( UdMnemonicCode.UD_Ipush, OpDefs.O_GS, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 1245 */ new UdItabEntry( UdMnemonicCode.UD_Ipush, OpDefs.O_FS, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 1246 */ new UdItabEntry( UdMnemonicCode.UD_Ipush, OpDefs.O_R0v, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso|BitOps.PRexb|BitOps.PDef64 ),
            /* 1247 */ new UdItabEntry( UdMnemonicCode.UD_Ipush, OpDefs.O_R1v, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso|BitOps.PRexb|BitOps.PDef64 ),
            /* 1248 */ new UdItabEntry( UdMnemonicCode.UD_Ipush, OpDefs.O_R2v, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso|BitOps.PRexb|BitOps.PDef64 ),
            /* 1249 */ new UdItabEntry( UdMnemonicCode.UD_Ipush, OpDefs.O_R3v, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso|BitOps.PRexb|BitOps.PDef64 ),
            /* 1250 */ new UdItabEntry( UdMnemonicCode.UD_Ipush, OpDefs.O_R4v, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso|BitOps.PRexb|BitOps.PDef64 ),
            /* 1251 */ new UdItabEntry( UdMnemonicCode.UD_Ipush, OpDefs.O_R5v, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso|BitOps.PRexb|BitOps.PDef64 ),
            /* 1252 */ new UdItabEntry( UdMnemonicCode.UD_Ipush, OpDefs.O_R6v, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso|BitOps.PRexb|BitOps.PDef64 ),
            /* 1253 */ new UdItabEntry( UdMnemonicCode.UD_Ipush, OpDefs.O_R7v, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso|BitOps.PRexb|BitOps.PDef64 ),
            /* 1254 */ new UdItabEntry( UdMnemonicCode.UD_Ipush, OpDefs.O_sIz, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso|BitOps.PDef64 ),
            /* 1255 */ new UdItabEntry( UdMnemonicCode.UD_Ipush, OpDefs.O_Ev, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb|BitOps.PDef64 ),
            /* 1256 */ new UdItabEntry( UdMnemonicCode.UD_Ipush, OpDefs.O_sIb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso|BitOps.PDef64 ),
            /* 1257 */ new UdItabEntry( UdMnemonicCode.UD_Ipusha, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso|BitOps.PInv64 ),
            /* 1258 */ new UdItabEntry( UdMnemonicCode.UD_Ipushad, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso|BitOps.PInv64 ),
            /* 1259 */ new UdItabEntry( UdMnemonicCode.UD_Ipushfw, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso ),
            /* 1260 */ new UdItabEntry( UdMnemonicCode.UD_Ipushfw, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso|BitOps.PRexw|BitOps.PDef64 ),
            /* 1261 */ new UdItabEntry( UdMnemonicCode.UD_Ipushfd, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso ),
            /* 1262 */ new UdItabEntry( UdMnemonicCode.UD_Ipushfq, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso|BitOps.PRexw|BitOps.PDef64 ),
            /* 1263 */ new UdItabEntry( UdMnemonicCode.UD_Ipushfq, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso|BitOps.PRexw|BitOps.PDef64 ),
            /* 1264 */ new UdItabEntry( UdMnemonicCode.UD_Ipxor, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1265 */ new UdItabEntry( UdMnemonicCode.UD_Ivpxor, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1266 */ new UdItabEntry( UdMnemonicCode.UD_Ipxor, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1267 */ new UdItabEntry( UdMnemonicCode.UD_Ircl, OpDefs.O_Eb, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1268 */ new UdItabEntry( UdMnemonicCode.UD_Ircl, OpDefs.O_Ev, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1269 */ new UdItabEntry( UdMnemonicCode.UD_Ircl, OpDefs.O_Eb, OpDefs.O_I1, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1270 */ new UdItabEntry( UdMnemonicCode.UD_Ircl, OpDefs.O_Eb, OpDefs.O_CL, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1271 */ new UdItabEntry( UdMnemonicCode.UD_Ircl, OpDefs.O_Ev, OpDefs.O_CL, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1272 */ new UdItabEntry( UdMnemonicCode.UD_Ircl, OpDefs.O_Ev, OpDefs.O_I1, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1273 */ new UdItabEntry( UdMnemonicCode.UD_Ircr, OpDefs.O_Eb, OpDefs.O_I1, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1274 */ new UdItabEntry( UdMnemonicCode.UD_Ircr, OpDefs.O_Ev, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1275 */ new UdItabEntry( UdMnemonicCode.UD_Ircr, OpDefs.O_Eb, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1276 */ new UdItabEntry( UdMnemonicCode.UD_Ircr, OpDefs.O_Ev, OpDefs.O_I1, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1277 */ new UdItabEntry( UdMnemonicCode.UD_Ircr, OpDefs.O_Eb, OpDefs.O_CL, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1278 */ new UdItabEntry( UdMnemonicCode.UD_Ircr, OpDefs.O_Ev, OpDefs.O_CL, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1279 */ new UdItabEntry( UdMnemonicCode.UD_Irol, OpDefs.O_Eb, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1280 */ new UdItabEntry( UdMnemonicCode.UD_Irol, OpDefs.O_Eb, OpDefs.O_I1, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1281 */ new UdItabEntry( UdMnemonicCode.UD_Irol, OpDefs.O_Ev, OpDefs.O_I1, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1282 */ new UdItabEntry( UdMnemonicCode.UD_Irol, OpDefs.O_Eb, OpDefs.O_CL, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1283 */ new UdItabEntry( UdMnemonicCode.UD_Irol, OpDefs.O_Ev, OpDefs.O_CL, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1284 */ new UdItabEntry( UdMnemonicCode.UD_Irol, OpDefs.O_Ev, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1285 */ new UdItabEntry( UdMnemonicCode.UD_Iror, OpDefs.O_Eb, OpDefs.O_I1, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1286 */ new UdItabEntry( UdMnemonicCode.UD_Iror, OpDefs.O_Eb, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1287 */ new UdItabEntry( UdMnemonicCode.UD_Iror, OpDefs.O_Ev, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1288 */ new UdItabEntry( UdMnemonicCode.UD_Iror, OpDefs.O_Ev, OpDefs.O_I1, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1289 */ new UdItabEntry( UdMnemonicCode.UD_Iror, OpDefs.O_Eb, OpDefs.O_CL, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1290 */ new UdItabEntry( UdMnemonicCode.UD_Iror, OpDefs.O_Ev, OpDefs.O_CL, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1291 */ new UdItabEntry( UdMnemonicCode.UD_Ircpps, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1292 */ new UdItabEntry( UdMnemonicCode.UD_Ivrcpps, OpDefs.O_Vx, OpDefs.O_Wx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb|BitOps.PVexl ),
            /* 1293 */ new UdItabEntry( UdMnemonicCode.UD_Ircpss, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1294 */ new UdItabEntry( UdMnemonicCode.UD_Ivrcpss, OpDefs.O_Vx, OpDefs.O_Wx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1295 */ new UdItabEntry( UdMnemonicCode.UD_Irdmsr, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 1296 */ new UdItabEntry( UdMnemonicCode.UD_Irdpmc, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 1297 */ new UdItabEntry( UdMnemonicCode.UD_Irdtsc, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 1298 */ new UdItabEntry( UdMnemonicCode.UD_Irdtscp, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 1299 */ new UdItabEntry( UdMnemonicCode.UD_Irepne, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 1300 */ new UdItabEntry( UdMnemonicCode.UD_Irep, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 1301 */ new UdItabEntry( UdMnemonicCode.UD_Iret, OpDefs.O_Iw, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 1302 */ new UdItabEntry( UdMnemonicCode.UD_Iret, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 1303 */ new UdItabEntry( UdMnemonicCode.UD_Iretf, OpDefs.O_Iw, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 1304 */ new UdItabEntry( UdMnemonicCode.UD_Iretf, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 1305 */ new UdItabEntry( UdMnemonicCode.UD_Irsm, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 1306 */ new UdItabEntry( UdMnemonicCode.UD_Irsqrtps, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1307 */ new UdItabEntry( UdMnemonicCode.UD_Ivrsqrtps, OpDefs.O_Vx, OpDefs.O_Wx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb|BitOps.PVexl ),
            /* 1308 */ new UdItabEntry( UdMnemonicCode.UD_Irsqrtss, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1309 */ new UdItabEntry( UdMnemonicCode.UD_Ivrsqrtss, OpDefs.O_Vx, OpDefs.O_Wx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1310 */ new UdItabEntry( UdMnemonicCode.UD_Isahf, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 1311 */ new UdItabEntry( UdMnemonicCode.UD_Isalc, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PInv64 ),
            /* 1312 */ new UdItabEntry( UdMnemonicCode.UD_Isar, OpDefs.O_Ev, OpDefs.O_I1, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1313 */ new UdItabEntry( UdMnemonicCode.UD_Isar, OpDefs.O_Eb, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1314 */ new UdItabEntry( UdMnemonicCode.UD_Isar, OpDefs.O_Eb, OpDefs.O_I1, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1315 */ new UdItabEntry( UdMnemonicCode.UD_Isar, OpDefs.O_Ev, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1316 */ new UdItabEntry( UdMnemonicCode.UD_Isar, OpDefs.O_Eb, OpDefs.O_CL, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1317 */ new UdItabEntry( UdMnemonicCode.UD_Isar, OpDefs.O_Ev, OpDefs.O_CL, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1318 */ new UdItabEntry( UdMnemonicCode.UD_Ishl, OpDefs.O_Eb, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1319 */ new UdItabEntry( UdMnemonicCode.UD_Ishl, OpDefs.O_Ev, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1320 */ new UdItabEntry( UdMnemonicCode.UD_Ishl, OpDefs.O_Eb, OpDefs.O_I1, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1321 */ new UdItabEntry( UdMnemonicCode.UD_Ishl, OpDefs.O_Eb, OpDefs.O_CL, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1322 */ new UdItabEntry( UdMnemonicCode.UD_Ishl, OpDefs.O_Ev, OpDefs.O_CL, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1323 */ new UdItabEntry( UdMnemonicCode.UD_Ishl, OpDefs.O_Ev, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1324 */ new UdItabEntry( UdMnemonicCode.UD_Ishl, OpDefs.O_Eb, OpDefs.O_CL, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1325 */ new UdItabEntry( UdMnemonicCode.UD_Ishl, OpDefs.O_Ev, OpDefs.O_I1, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1326 */ new UdItabEntry( UdMnemonicCode.UD_Ishl, OpDefs.O_Eb, OpDefs.O_I1, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1327 */ new UdItabEntry( UdMnemonicCode.UD_Ishl, OpDefs.O_Eb, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1328 */ new UdItabEntry( UdMnemonicCode.UD_Ishl, OpDefs.O_Ev, OpDefs.O_CL, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1329 */ new UdItabEntry( UdMnemonicCode.UD_Ishl, OpDefs.O_Ev, OpDefs.O_I1, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1330 */ new UdItabEntry( UdMnemonicCode.UD_Ishr, OpDefs.O_Ev, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1331 */ new UdItabEntry( UdMnemonicCode.UD_Ishr, OpDefs.O_Eb, OpDefs.O_CL, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1332 */ new UdItabEntry( UdMnemonicCode.UD_Ishr, OpDefs.O_Ev, OpDefs.O_I1, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1333 */ new UdItabEntry( UdMnemonicCode.UD_Ishr, OpDefs.O_Eb, OpDefs.O_I1, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1334 */ new UdItabEntry( UdMnemonicCode.UD_Ishr, OpDefs.O_Eb, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1335 */ new UdItabEntry( UdMnemonicCode.UD_Ishr, OpDefs.O_Ev, OpDefs.O_CL, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1336 */ new UdItabEntry( UdMnemonicCode.UD_Isbb, OpDefs.O_Eb, OpDefs.O_Gb, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1337 */ new UdItabEntry( UdMnemonicCode.UD_Isbb, OpDefs.O_Ev, OpDefs.O_Gv, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1338 */ new UdItabEntry( UdMnemonicCode.UD_Isbb, OpDefs.O_Gb, OpDefs.O_Eb, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1339 */ new UdItabEntry( UdMnemonicCode.UD_Isbb, OpDefs.O_Gv, OpDefs.O_Ev, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1340 */ new UdItabEntry( UdMnemonicCode.UD_Isbb, OpDefs.O_AL, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 1341 */ new UdItabEntry( UdMnemonicCode.UD_Isbb, OpDefs.O_rAX, OpDefs.O_sIz, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso|BitOps.PRexw ),
            /* 1342 */ new UdItabEntry( UdMnemonicCode.UD_Isbb, OpDefs.O_Eb, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1343 */ new UdItabEntry( UdMnemonicCode.UD_Isbb, OpDefs.O_Ev, OpDefs.O_sIz, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1344 */ new UdItabEntry( UdMnemonicCode.UD_Isbb, OpDefs.O_Eb, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb|BitOps.PInv64 ),
            /* 1345 */ new UdItabEntry( UdMnemonicCode.UD_Isbb, OpDefs.O_Ev, OpDefs.O_sIb, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1346 */ new UdItabEntry( UdMnemonicCode.UD_Iscasb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PStrz ),
            /* 1347 */ new UdItabEntry( UdMnemonicCode.UD_Iscasw, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PStrz|BitOps.POso|BitOps.PRexw ),
            /* 1348 */ new UdItabEntry( UdMnemonicCode.UD_Iscasd, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PStrz|BitOps.POso|BitOps.PRexw ),
            /* 1349 */ new UdItabEntry( UdMnemonicCode.UD_Iscasq, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PStrz|BitOps.POso|BitOps.PRexw ),
            /* 1350 */ new UdItabEntry( UdMnemonicCode.UD_Iseto, OpDefs.O_Eb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1351 */ new UdItabEntry( UdMnemonicCode.UD_Isetno, OpDefs.O_Eb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1352 */ new UdItabEntry( UdMnemonicCode.UD_Isetb, OpDefs.O_Eb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1353 */ new UdItabEntry( UdMnemonicCode.UD_Isetae, OpDefs.O_Eb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1354 */ new UdItabEntry( UdMnemonicCode.UD_Isetz, OpDefs.O_Eb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1355 */ new UdItabEntry( UdMnemonicCode.UD_Isetnz, OpDefs.O_Eb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1356 */ new UdItabEntry( UdMnemonicCode.UD_Isetbe, OpDefs.O_Eb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1357 */ new UdItabEntry( UdMnemonicCode.UD_Iseta, OpDefs.O_Eb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1358 */ new UdItabEntry( UdMnemonicCode.UD_Isets, OpDefs.O_Eb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1359 */ new UdItabEntry( UdMnemonicCode.UD_Isetns, OpDefs.O_Eb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1360 */ new UdItabEntry( UdMnemonicCode.UD_Isetp, OpDefs.O_Eb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1361 */ new UdItabEntry( UdMnemonicCode.UD_Isetnp, OpDefs.O_Eb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1362 */ new UdItabEntry( UdMnemonicCode.UD_Isetl, OpDefs.O_Eb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1363 */ new UdItabEntry( UdMnemonicCode.UD_Isetge, OpDefs.O_Eb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1364 */ new UdItabEntry( UdMnemonicCode.UD_Isetle, OpDefs.O_Eb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1365 */ new UdItabEntry( UdMnemonicCode.UD_Isetg, OpDefs.O_Eb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1366 */ new UdItabEntry( UdMnemonicCode.UD_Isfence, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 1367 */ new UdItabEntry( UdMnemonicCode.UD_Isfence, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 1368 */ new UdItabEntry( UdMnemonicCode.UD_Isfence, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 1369 */ new UdItabEntry( UdMnemonicCode.UD_Isfence, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 1370 */ new UdItabEntry( UdMnemonicCode.UD_Isfence, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 1371 */ new UdItabEntry( UdMnemonicCode.UD_Isfence, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 1372 */ new UdItabEntry( UdMnemonicCode.UD_Isfence, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 1373 */ new UdItabEntry( UdMnemonicCode.UD_Isfence, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 1374 */ new UdItabEntry( UdMnemonicCode.UD_Isgdt, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1375 */ new UdItabEntry( UdMnemonicCode.UD_Ishld, OpDefs.O_Ev, OpDefs.O_Gv, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1376 */ new UdItabEntry( UdMnemonicCode.UD_Ishld, OpDefs.O_Ev, OpDefs.O_Gv, OpDefs.O_CL, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1377 */ new UdItabEntry( UdMnemonicCode.UD_Ishrd, OpDefs.O_Ev, OpDefs.O_Gv, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1378 */ new UdItabEntry( UdMnemonicCode.UD_Ishrd, OpDefs.O_Ev, OpDefs.O_Gv, OpDefs.O_CL, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1379 */ new UdItabEntry( UdMnemonicCode.UD_Ishufpd, OpDefs.O_V, OpDefs.O_W, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1380 */ new UdItabEntry( UdMnemonicCode.UD_Ivshufpd, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_Ib, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb|BitOps.PVexl ),
            /* 1381 */ new UdItabEntry( UdMnemonicCode.UD_Ishufps, OpDefs.O_V, OpDefs.O_W, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1382 */ new UdItabEntry( UdMnemonicCode.UD_Ivshufps, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_Ib, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1383 */ new UdItabEntry( UdMnemonicCode.UD_Isidt, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1384 */ new UdItabEntry( UdMnemonicCode.UD_Isldt, OpDefs.O_MwRv, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexr|BitOps.PRexw|BitOps.PRexx|BitOps.PRexb ),
            /* 1385 */ new UdItabEntry( UdMnemonicCode.UD_Ismsw, OpDefs.O_MwRv, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexr|BitOps.PRexw|BitOps.PRexx|BitOps.PRexb ),
            /* 1386 */ new UdItabEntry( UdMnemonicCode.UD_Ismsw, OpDefs.O_MwRv, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexr|BitOps.PRexw|BitOps.PRexx|BitOps.PRexb ),
            /* 1387 */ new UdItabEntry( UdMnemonicCode.UD_Isqrtps, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1388 */ new UdItabEntry( UdMnemonicCode.UD_Ivsqrtps, OpDefs.O_Vx, OpDefs.O_Wx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb|BitOps.PVexl ),
            /* 1389 */ new UdItabEntry( UdMnemonicCode.UD_Isqrtpd, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1390 */ new UdItabEntry( UdMnemonicCode.UD_Ivsqrtpd, OpDefs.O_Vx, OpDefs.O_Wx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1391 */ new UdItabEntry( UdMnemonicCode.UD_Isqrtsd, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1392 */ new UdItabEntry( UdMnemonicCode.UD_Ivsqrtsd, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1393 */ new UdItabEntry( UdMnemonicCode.UD_Isqrtss, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1394 */ new UdItabEntry( UdMnemonicCode.UD_Ivsqrtss, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1395 */ new UdItabEntry( UdMnemonicCode.UD_Istc, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 1396 */ new UdItabEntry( UdMnemonicCode.UD_Istd, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 1397 */ new UdItabEntry( UdMnemonicCode.UD_Istgi, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 1398 */ new UdItabEntry( UdMnemonicCode.UD_Isti, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 1399 */ new UdItabEntry( UdMnemonicCode.UD_Iskinit, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 1400 */ new UdItabEntry( UdMnemonicCode.UD_Istmxcsr, OpDefs.O_Md, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1401 */ new UdItabEntry( UdMnemonicCode.UD_Ivstmxcsr, OpDefs.O_Md, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1402 */ new UdItabEntry( UdMnemonicCode.UD_Istosb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PStr|BitOps.PSeg ),
            /* 1403 */ new UdItabEntry( UdMnemonicCode.UD_Istosw, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PStr|BitOps.PSeg|BitOps.POso|BitOps.PRexw ),
            /* 1404 */ new UdItabEntry( UdMnemonicCode.UD_Istosd, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PStr|BitOps.PSeg|BitOps.POso|BitOps.PRexw ),
            /* 1405 */ new UdItabEntry( UdMnemonicCode.UD_Istosq, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PStr|BitOps.PSeg|BitOps.POso|BitOps.PRexw ),
            /* 1406 */ new UdItabEntry( UdMnemonicCode.UD_Istr, OpDefs.O_MwRv, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexr|BitOps.PRexw|BitOps.PRexx|BitOps.PRexb ),
            /* 1407 */ new UdItabEntry( UdMnemonicCode.UD_Isub, OpDefs.O_Eb, OpDefs.O_Gb, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1408 */ new UdItabEntry( UdMnemonicCode.UD_Isub, OpDefs.O_Ev, OpDefs.O_Gv, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1409 */ new UdItabEntry( UdMnemonicCode.UD_Isub, OpDefs.O_Gb, OpDefs.O_Eb, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1410 */ new UdItabEntry( UdMnemonicCode.UD_Isub, OpDefs.O_Gv, OpDefs.O_Ev, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1411 */ new UdItabEntry( UdMnemonicCode.UD_Isub, OpDefs.O_AL, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 1412 */ new UdItabEntry( UdMnemonicCode.UD_Isub, OpDefs.O_rAX, OpDefs.O_sIz, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso|BitOps.PRexw ),
            /* 1413 */ new UdItabEntry( UdMnemonicCode.UD_Isub, OpDefs.O_Eb, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1414 */ new UdItabEntry( UdMnemonicCode.UD_Isub, OpDefs.O_Ev, OpDefs.O_sIz, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1415 */ new UdItabEntry( UdMnemonicCode.UD_Isub, OpDefs.O_Eb, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb|BitOps.PInv64 ),
            /* 1416 */ new UdItabEntry( UdMnemonicCode.UD_Isub, OpDefs.O_Ev, OpDefs.O_sIb, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1417 */ new UdItabEntry( UdMnemonicCode.UD_Isubpd, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1418 */ new UdItabEntry( UdMnemonicCode.UD_Ivsubpd, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb|BitOps.PVexl ),
            /* 1419 */ new UdItabEntry( UdMnemonicCode.UD_Isubps, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1420 */ new UdItabEntry( UdMnemonicCode.UD_Ivsubps, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb|BitOps.PVexl ),
            /* 1421 */ new UdItabEntry( UdMnemonicCode.UD_Isubsd, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1422 */ new UdItabEntry( UdMnemonicCode.UD_Ivsubsd, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1423 */ new UdItabEntry( UdMnemonicCode.UD_Isubss, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1424 */ new UdItabEntry( UdMnemonicCode.UD_Ivsubss, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1425 */ new UdItabEntry( UdMnemonicCode.UD_Iswapgs, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 1426 */ new UdItabEntry( UdMnemonicCode.UD_Isyscall, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 1427 */ new UdItabEntry( UdMnemonicCode.UD_Isysenter, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 1428 */ new UdItabEntry( UdMnemonicCode.UD_Isysenter, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 1429 */ new UdItabEntry( UdMnemonicCode.UD_Isysexit, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 1430 */ new UdItabEntry( UdMnemonicCode.UD_Isysexit, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 1431 */ new UdItabEntry( UdMnemonicCode.UD_Isysret, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 1432 */ new UdItabEntry( UdMnemonicCode.UD_Itest, OpDefs.O_Eb, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1433 */ new UdItabEntry( UdMnemonicCode.UD_Itest, OpDefs.O_Eb, OpDefs.O_Gb, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1434 */ new UdItabEntry( UdMnemonicCode.UD_Itest, OpDefs.O_Ev, OpDefs.O_Gv, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1435 */ new UdItabEntry( UdMnemonicCode.UD_Itest, OpDefs.O_AL, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 1436 */ new UdItabEntry( UdMnemonicCode.UD_Itest, OpDefs.O_rAX, OpDefs.O_sIz, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso|BitOps.PRexw ),
            /* 1437 */ new UdItabEntry( UdMnemonicCode.UD_Itest, OpDefs.O_Eb, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1438 */ new UdItabEntry( UdMnemonicCode.UD_Itest, OpDefs.O_Ev, OpDefs.O_sIz, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1439 */ new UdItabEntry( UdMnemonicCode.UD_Itest, OpDefs.O_Ev, OpDefs.O_Iz, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1440 */ new UdItabEntry( UdMnemonicCode.UD_Iucomisd, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1441 */ new UdItabEntry( UdMnemonicCode.UD_Ivucomisd, OpDefs.O_Vx, OpDefs.O_Wx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1442 */ new UdItabEntry( UdMnemonicCode.UD_Iucomiss, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1443 */ new UdItabEntry( UdMnemonicCode.UD_Ivucomiss, OpDefs.O_Vx, OpDefs.O_Wx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1444 */ new UdItabEntry( UdMnemonicCode.UD_Iud2, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 1445 */ new UdItabEntry( UdMnemonicCode.UD_Iunpckhpd, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1446 */ new UdItabEntry( UdMnemonicCode.UD_Ivunpckhpd, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb|BitOps.PVexl ),
            /* 1447 */ new UdItabEntry( UdMnemonicCode.UD_Iunpckhps, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1448 */ new UdItabEntry( UdMnemonicCode.UD_Ivunpckhps, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1449 */ new UdItabEntry( UdMnemonicCode.UD_Iunpcklps, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1450 */ new UdItabEntry( UdMnemonicCode.UD_Ivunpcklps, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1451 */ new UdItabEntry( UdMnemonicCode.UD_Iunpcklpd, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1452 */ new UdItabEntry( UdMnemonicCode.UD_Ivunpcklpd, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb|BitOps.PVexl ),
            /* 1453 */ new UdItabEntry( UdMnemonicCode.UD_Iverr, OpDefs.O_Ew, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1454 */ new UdItabEntry( UdMnemonicCode.UD_Iverw, OpDefs.O_Ew, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1455 */ new UdItabEntry( UdMnemonicCode.UD_Ivmcall, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 1456 */ new UdItabEntry( UdMnemonicCode.UD_Irdrand, OpDefs.O_R, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso|BitOps.PRexr|BitOps.PRexw|BitOps.PRexx|BitOps.PRexb ),
            /* 1457 */ new UdItabEntry( UdMnemonicCode.UD_Ivmclear, OpDefs.O_Mq, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1458 */ new UdItabEntry( UdMnemonicCode.UD_Ivmxon, OpDefs.O_Mq, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1459 */ new UdItabEntry( UdMnemonicCode.UD_Ivmptrld, OpDefs.O_Mq, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1460 */ new UdItabEntry( UdMnemonicCode.UD_Ivmptrst, OpDefs.O_Mq, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1461 */ new UdItabEntry( UdMnemonicCode.UD_Ivmlaunch, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 1462 */ new UdItabEntry( UdMnemonicCode.UD_Ivmresume, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 1463 */ new UdItabEntry( UdMnemonicCode.UD_Ivmxoff, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 1464 */ new UdItabEntry( UdMnemonicCode.UD_Ivmread, OpDefs.O_Ey, OpDefs.O_Gy, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb|BitOps.PDef64 ),
            /* 1465 */ new UdItabEntry( UdMnemonicCode.UD_Ivmwrite, OpDefs.O_Gy, OpDefs.O_Ey, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb|BitOps.PDef64 ),
            /* 1466 */ new UdItabEntry( UdMnemonicCode.UD_Ivmrun, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 1467 */ new UdItabEntry( UdMnemonicCode.UD_Ivmmcall, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 1468 */ new UdItabEntry( UdMnemonicCode.UD_Ivmload, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 1469 */ new UdItabEntry( UdMnemonicCode.UD_Ivmsave, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 1470 */ new UdItabEntry( UdMnemonicCode.UD_Iwait, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 1471 */ new UdItabEntry( UdMnemonicCode.UD_Iwbinvd, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 1472 */ new UdItabEntry( UdMnemonicCode.UD_Iwrmsr, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 1473 */ new UdItabEntry( UdMnemonicCode.UD_Ixadd, OpDefs.O_Eb, OpDefs.O_Gb, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1474 */ new UdItabEntry( UdMnemonicCode.UD_Ixadd, OpDefs.O_Ev, OpDefs.O_Gv, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1475 */ new UdItabEntry( UdMnemonicCode.UD_Ixchg, OpDefs.O_Eb, OpDefs.O_Gb, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1476 */ new UdItabEntry( UdMnemonicCode.UD_Ixchg, OpDefs.O_Ev, OpDefs.O_Gv, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1477 */ new UdItabEntry( UdMnemonicCode.UD_Ixchg, OpDefs.O_R0v, OpDefs.O_rAX, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso|BitOps.PRexw|BitOps.PRexb ),
            /* 1478 */ new UdItabEntry( UdMnemonicCode.UD_Ixchg, OpDefs.O_R1v, OpDefs.O_rAX, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso|BitOps.PRexw|BitOps.PRexb ),
            /* 1479 */ new UdItabEntry( UdMnemonicCode.UD_Ixchg, OpDefs.O_R2v, OpDefs.O_rAX, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso|BitOps.PRexw|BitOps.PRexb ),
            /* 1480 */ new UdItabEntry( UdMnemonicCode.UD_Ixchg, OpDefs.O_R3v, OpDefs.O_rAX, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso|BitOps.PRexw|BitOps.PRexb ),
            /* 1481 */ new UdItabEntry( UdMnemonicCode.UD_Ixchg, OpDefs.O_R4v, OpDefs.O_rAX, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso|BitOps.PRexw|BitOps.PRexb ),
            /* 1482 */ new UdItabEntry( UdMnemonicCode.UD_Ixchg, OpDefs.O_R5v, OpDefs.O_rAX, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso|BitOps.PRexw|BitOps.PRexb ),
            /* 1483 */ new UdItabEntry( UdMnemonicCode.UD_Ixchg, OpDefs.O_R6v, OpDefs.O_rAX, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso|BitOps.PRexw|BitOps.PRexb ),
            /* 1484 */ new UdItabEntry( UdMnemonicCode.UD_Ixchg, OpDefs.O_R7v, OpDefs.O_rAX, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso|BitOps.PRexw|BitOps.PRexb ),
            /* 1485 */ new UdItabEntry( UdMnemonicCode.UD_Ixgetbv, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 1486 */ new UdItabEntry( UdMnemonicCode.UD_Ixlatb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PRexw|BitOps.PSeg ),
            /* 1487 */ new UdItabEntry( UdMnemonicCode.UD_Ixor, OpDefs.O_Eb, OpDefs.O_Gb, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1488 */ new UdItabEntry( UdMnemonicCode.UD_Ixor, OpDefs.O_Ev, OpDefs.O_Gv, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1489 */ new UdItabEntry( UdMnemonicCode.UD_Ixor, OpDefs.O_Gb, OpDefs.O_Eb, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1490 */ new UdItabEntry( UdMnemonicCode.UD_Ixor, OpDefs.O_Gv, OpDefs.O_Ev, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1491 */ new UdItabEntry( UdMnemonicCode.UD_Ixor, OpDefs.O_AL, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 1492 */ new UdItabEntry( UdMnemonicCode.UD_Ixor, OpDefs.O_rAX, OpDefs.O_sIz, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso|BitOps.PRexw ),
            /* 1493 */ new UdItabEntry( UdMnemonicCode.UD_Ixor, OpDefs.O_Eb, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1494 */ new UdItabEntry( UdMnemonicCode.UD_Ixor, OpDefs.O_Ev, OpDefs.O_sIz, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1495 */ new UdItabEntry( UdMnemonicCode.UD_Ixor, OpDefs.O_Eb, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb|BitOps.PInv64 ),
            /* 1496 */ new UdItabEntry( UdMnemonicCode.UD_Ixor, OpDefs.O_Ev, OpDefs.O_sIb, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1497 */ new UdItabEntry( UdMnemonicCode.UD_Ixorpd, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1498 */ new UdItabEntry( UdMnemonicCode.UD_Ivxorpd, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb|BitOps.PVexl ),
            /* 1499 */ new UdItabEntry( UdMnemonicCode.UD_Ixorps, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1500 */ new UdItabEntry( UdMnemonicCode.UD_Ivxorps, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1501 */ new UdItabEntry( UdMnemonicCode.UD_Ixcryptecb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 1502 */ new UdItabEntry( UdMnemonicCode.UD_Ixcryptcbc, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 1503 */ new UdItabEntry( UdMnemonicCode.UD_Ixcryptctr, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 1504 */ new UdItabEntry( UdMnemonicCode.UD_Ixcryptcfb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 1505 */ new UdItabEntry( UdMnemonicCode.UD_Ixcryptofb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 1506 */ new UdItabEntry( UdMnemonicCode.UD_Ixrstor, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1507 */ new UdItabEntry( UdMnemonicCode.UD_Ixsave, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1508 */ new UdItabEntry( UdMnemonicCode.UD_Ixsetbv, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 1509 */ new UdItabEntry( UdMnemonicCode.UD_Ixsha1, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 1510 */ new UdItabEntry( UdMnemonicCode.UD_Ixsha256, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 1511 */ new UdItabEntry( UdMnemonicCode.UD_Ixstore, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 1512 */ new UdItabEntry( UdMnemonicCode.UD_Ipclmulqdq, OpDefs.O_V, OpDefs.O_W, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1513 */ new UdItabEntry( UdMnemonicCode.UD_Ivpclmulqdq, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_Ib, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1514 */ new UdItabEntry( UdMnemonicCode.UD_Igetsec, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 1515 */ new UdItabEntry( UdMnemonicCode.UD_Imovdqa, OpDefs.O_W, OpDefs.O_V, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1516 */ new UdItabEntry( UdMnemonicCode.UD_Ivmovdqa, OpDefs.O_Wx, OpDefs.O_Vx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb|BitOps.PVexl ),
            /* 1517 */ new UdItabEntry( UdMnemonicCode.UD_Imovdqa, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1518 */ new UdItabEntry( UdMnemonicCode.UD_Ivmovdqa, OpDefs.O_Vx, OpDefs.O_Wx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb|BitOps.PVexl ),
            /* 1519 */ new UdItabEntry( UdMnemonicCode.UD_Imaskmovdqu, OpDefs.O_V, OpDefs.O_U, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1520 */ new UdItabEntry( UdMnemonicCode.UD_Ivmaskmovdqu, OpDefs.O_Vx, OpDefs.O_Ux, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1521 */ new UdItabEntry( UdMnemonicCode.UD_Imovdq2q, OpDefs.O_P, OpDefs.O_U, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexb ),
            /* 1522 */ new UdItabEntry( UdMnemonicCode.UD_Imovdqu, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1523 */ new UdItabEntry( UdMnemonicCode.UD_Ivmovdqu, OpDefs.O_Vx, OpDefs.O_Wx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb|BitOps.PVexl ),
            /* 1524 */ new UdItabEntry( UdMnemonicCode.UD_Imovdqu, OpDefs.O_W, OpDefs.O_V, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1525 */ new UdItabEntry( UdMnemonicCode.UD_Ivmovdqu, OpDefs.O_Wx, OpDefs.O_Vx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb|BitOps.PVexl ),
            /* 1526 */ new UdItabEntry( UdMnemonicCode.UD_Imovq2dq, OpDefs.O_V, OpDefs.O_N, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr ),
            /* 1527 */ new UdItabEntry( UdMnemonicCode.UD_Ipaddq, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1528 */ new UdItabEntry( UdMnemonicCode.UD_Ipaddq, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1529 */ new UdItabEntry( UdMnemonicCode.UD_Ivpaddq, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1530 */ new UdItabEntry( UdMnemonicCode.UD_Ipsubq, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1531 */ new UdItabEntry( UdMnemonicCode.UD_Ivpsubq, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1532 */ new UdItabEntry( UdMnemonicCode.UD_Ipsubq, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1533 */ new UdItabEntry( UdMnemonicCode.UD_Ipmuludq, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1534 */ new UdItabEntry( UdMnemonicCode.UD_Ipmuludq, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1535 */ new UdItabEntry( UdMnemonicCode.UD_Ipshufhw, OpDefs.O_V, OpDefs.O_W, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1536 */ new UdItabEntry( UdMnemonicCode.UD_Ivpshufhw, OpDefs.O_Vx, OpDefs.O_Wx, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1537 */ new UdItabEntry( UdMnemonicCode.UD_Ipshuflw, OpDefs.O_V, OpDefs.O_W, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1538 */ new UdItabEntry( UdMnemonicCode.UD_Ivpshuflw, OpDefs.O_Vx, OpDefs.O_Wx, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1539 */ new UdItabEntry( UdMnemonicCode.UD_Ipshufd, OpDefs.O_V, OpDefs.O_W, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1540 */ new UdItabEntry( UdMnemonicCode.UD_Ivpshufd, OpDefs.O_Vx, OpDefs.O_Wx, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1541 */ new UdItabEntry( UdMnemonicCode.UD_Ipslldq, OpDefs.O_U, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PRexb ),
            /* 1542 */ new UdItabEntry( UdMnemonicCode.UD_Ivpslldq, OpDefs.O_Hx, OpDefs.O_Ux, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.PRexb ),
            /* 1543 */ new UdItabEntry( UdMnemonicCode.UD_Ipsrldq, OpDefs.O_U, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PRexb ),
            /* 1544 */ new UdItabEntry( UdMnemonicCode.UD_Ivpsrldq, OpDefs.O_Hx, OpDefs.O_Ux, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.PRexb ),
            /* 1545 */ new UdItabEntry( UdMnemonicCode.UD_Ipunpckhqdq, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1546 */ new UdItabEntry( UdMnemonicCode.UD_Ivpunpckhqdq, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1547 */ new UdItabEntry( UdMnemonicCode.UD_Ipunpcklqdq, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1548 */ new UdItabEntry( UdMnemonicCode.UD_Ivpunpcklqdq, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1549 */ new UdItabEntry( UdMnemonicCode.UD_Ihaddpd, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1550 */ new UdItabEntry( UdMnemonicCode.UD_Ivhaddpd, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb|BitOps.PVexl ),
            /* 1551 */ new UdItabEntry( UdMnemonicCode.UD_Ihaddps, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1552 */ new UdItabEntry( UdMnemonicCode.UD_Ivhaddps, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb|BitOps.PVexl ),
            /* 1553 */ new UdItabEntry( UdMnemonicCode.UD_Ihsubpd, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1554 */ new UdItabEntry( UdMnemonicCode.UD_Ivhsubpd, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb|BitOps.PVexl ),
            /* 1555 */ new UdItabEntry( UdMnemonicCode.UD_Ihsubps, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1556 */ new UdItabEntry( UdMnemonicCode.UD_Ivhsubps, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb|BitOps.PVexl ),
            /* 1557 */ new UdItabEntry( UdMnemonicCode.UD_Iinsertps, OpDefs.O_V, OpDefs.O_Md, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexw|BitOps.PRexx|BitOps.PRexb ),
            /* 1558 */ new UdItabEntry( UdMnemonicCode.UD_Ivinsertps, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Md, OpDefs.O_Ib, BitOps.PAso|BitOps.PRexr|BitOps.PRexw|BitOps.PRexx|BitOps.PRexb ),
            /* 1559 */ new UdItabEntry( UdMnemonicCode.UD_Ilddqu, OpDefs.O_V, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1560 */ new UdItabEntry( UdMnemonicCode.UD_Ivlddqu, OpDefs.O_Vx, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb|BitOps.PVexl ),
            /* 1561 */ new UdItabEntry( UdMnemonicCode.UD_Imovddup, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1562 */ new UdItabEntry( UdMnemonicCode.UD_Ivmovddup, OpDefs.O_Vx, OpDefs.O_Wx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1563 */ new UdItabEntry( UdMnemonicCode.UD_Imovddup, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1564 */ new UdItabEntry( UdMnemonicCode.UD_Ivmovddup, OpDefs.O_Vx, OpDefs.O_Wx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1565 */ new UdItabEntry( UdMnemonicCode.UD_Imovshdup, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1566 */ new UdItabEntry( UdMnemonicCode.UD_Ivmovshdup, OpDefs.O_Vx, OpDefs.O_Wx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb|BitOps.PVexl ),
            /* 1567 */ new UdItabEntry( UdMnemonicCode.UD_Imovshdup, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1568 */ new UdItabEntry( UdMnemonicCode.UD_Ivmovshdup, OpDefs.O_Vx, OpDefs.O_Wx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb|BitOps.PVexl ),
            /* 1569 */ new UdItabEntry( UdMnemonicCode.UD_Imovsldup, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1570 */ new UdItabEntry( UdMnemonicCode.UD_Ivmovsldup, OpDefs.O_Vx, OpDefs.O_Wx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb|BitOps.PVexl ),
            /* 1571 */ new UdItabEntry( UdMnemonicCode.UD_Imovsldup, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1572 */ new UdItabEntry( UdMnemonicCode.UD_Ivmovsldup, OpDefs.O_Vx, OpDefs.O_Wx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb|BitOps.PVexl ),
            /* 1573 */ new UdItabEntry( UdMnemonicCode.UD_Ipabsb, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1574 */ new UdItabEntry( UdMnemonicCode.UD_Ipabsb, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1575 */ new UdItabEntry( UdMnemonicCode.UD_Ivpabsb, OpDefs.O_Vx, OpDefs.O_Wx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb|BitOps.PVexl ),
            /* 1576 */ new UdItabEntry( UdMnemonicCode.UD_Ipabsw, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1577 */ new UdItabEntry( UdMnemonicCode.UD_Ipabsw, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1578 */ new UdItabEntry( UdMnemonicCode.UD_Ivpabsw, OpDefs.O_Vx, OpDefs.O_Wx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb|BitOps.PVexl ),
            /* 1579 */ new UdItabEntry( UdMnemonicCode.UD_Ipabsd, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1580 */ new UdItabEntry( UdMnemonicCode.UD_Ipabsd, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1581 */ new UdItabEntry( UdMnemonicCode.UD_Ivpabsd, OpDefs.O_Vx, OpDefs.O_Wx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb|BitOps.PVexl ),
            /* 1582 */ new UdItabEntry( UdMnemonicCode.UD_Ipshufb, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1583 */ new UdItabEntry( UdMnemonicCode.UD_Ipshufb, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1584 */ new UdItabEntry( UdMnemonicCode.UD_Ivpshufb, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1585 */ new UdItabEntry( UdMnemonicCode.UD_Iphaddw, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1586 */ new UdItabEntry( UdMnemonicCode.UD_Iphaddw, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1587 */ new UdItabEntry( UdMnemonicCode.UD_Ivphaddw, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1588 */ new UdItabEntry( UdMnemonicCode.UD_Iphaddd, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1589 */ new UdItabEntry( UdMnemonicCode.UD_Iphaddd, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1590 */ new UdItabEntry( UdMnemonicCode.UD_Ivphaddd, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1591 */ new UdItabEntry( UdMnemonicCode.UD_Iphaddsw, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1592 */ new UdItabEntry( UdMnemonicCode.UD_Iphaddsw, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1593 */ new UdItabEntry( UdMnemonicCode.UD_Ivphaddsw, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1594 */ new UdItabEntry( UdMnemonicCode.UD_Ipmaddubsw, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1595 */ new UdItabEntry( UdMnemonicCode.UD_Ipmaddubsw, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1596 */ new UdItabEntry( UdMnemonicCode.UD_Ivpmaddubsw, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1597 */ new UdItabEntry( UdMnemonicCode.UD_Iphsubw, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1598 */ new UdItabEntry( UdMnemonicCode.UD_Iphsubw, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1599 */ new UdItabEntry( UdMnemonicCode.UD_Ivphsubw, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1600 */ new UdItabEntry( UdMnemonicCode.UD_Iphsubd, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1601 */ new UdItabEntry( UdMnemonicCode.UD_Iphsubd, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1602 */ new UdItabEntry( UdMnemonicCode.UD_Ivphsubd, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1603 */ new UdItabEntry( UdMnemonicCode.UD_Iphsubsw, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1604 */ new UdItabEntry( UdMnemonicCode.UD_Iphsubsw, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1605 */ new UdItabEntry( UdMnemonicCode.UD_Ivphsubsw, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1606 */ new UdItabEntry( UdMnemonicCode.UD_Ipsignb, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1607 */ new UdItabEntry( UdMnemonicCode.UD_Ipsignb, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1608 */ new UdItabEntry( UdMnemonicCode.UD_Ivpsignb, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1609 */ new UdItabEntry( UdMnemonicCode.UD_Ipsignd, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1610 */ new UdItabEntry( UdMnemonicCode.UD_Ipsignd, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1611 */ new UdItabEntry( UdMnemonicCode.UD_Ivpsignd, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1612 */ new UdItabEntry( UdMnemonicCode.UD_Ipsignw, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1613 */ new UdItabEntry( UdMnemonicCode.UD_Ipsignw, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1614 */ new UdItabEntry( UdMnemonicCode.UD_Ivpsignw, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1615 */ new UdItabEntry( UdMnemonicCode.UD_Ipmulhrsw, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1616 */ new UdItabEntry( UdMnemonicCode.UD_Ipmulhrsw, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1617 */ new UdItabEntry( UdMnemonicCode.UD_Ivpmulhrsw, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1618 */ new UdItabEntry( UdMnemonicCode.UD_Ipalignr, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1619 */ new UdItabEntry( UdMnemonicCode.UD_Ipalignr, OpDefs.O_V, OpDefs.O_W, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1620 */ new UdItabEntry( UdMnemonicCode.UD_Ivpalignr, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_Ib, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1621 */ new UdItabEntry( UdMnemonicCode.UD_Ipblendvb, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1622 */ new UdItabEntry( UdMnemonicCode.UD_Ipmuldq, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1623 */ new UdItabEntry( UdMnemonicCode.UD_Ivpmuldq, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1624 */ new UdItabEntry( UdMnemonicCode.UD_Ipminsb, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1625 */ new UdItabEntry( UdMnemonicCode.UD_Ivpminsb, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1626 */ new UdItabEntry( UdMnemonicCode.UD_Ipminsd, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1627 */ new UdItabEntry( UdMnemonicCode.UD_Ivpminsd, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1628 */ new UdItabEntry( UdMnemonicCode.UD_Ipminuw, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1629 */ new UdItabEntry( UdMnemonicCode.UD_Ivpminuw, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1630 */ new UdItabEntry( UdMnemonicCode.UD_Ipminud, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1631 */ new UdItabEntry( UdMnemonicCode.UD_Ivpminud, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1632 */ new UdItabEntry( UdMnemonicCode.UD_Ipmaxsb, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1633 */ new UdItabEntry( UdMnemonicCode.UD_Ivpmaxsb, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1634 */ new UdItabEntry( UdMnemonicCode.UD_Ipmaxsd, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1635 */ new UdItabEntry( UdMnemonicCode.UD_Ivpmaxsd, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1636 */ new UdItabEntry( UdMnemonicCode.UD_Ipmaxud, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1637 */ new UdItabEntry( UdMnemonicCode.UD_Ivpmaxud, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1638 */ new UdItabEntry( UdMnemonicCode.UD_Ipmaxuw, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1639 */ new UdItabEntry( UdMnemonicCode.UD_Ivpmaxuw, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1640 */ new UdItabEntry( UdMnemonicCode.UD_Ipmulld, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1641 */ new UdItabEntry( UdMnemonicCode.UD_Ivpmulld, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1642 */ new UdItabEntry( UdMnemonicCode.UD_Iphminposuw, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1643 */ new UdItabEntry( UdMnemonicCode.UD_Ivphminposuw, OpDefs.O_Vx, OpDefs.O_Wx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1644 */ new UdItabEntry( UdMnemonicCode.UD_Iroundps, OpDefs.O_V, OpDefs.O_W, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1645 */ new UdItabEntry( UdMnemonicCode.UD_Ivroundps, OpDefs.O_Vx, OpDefs.O_Wx, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb|BitOps.PVexl ),
            /* 1646 */ new UdItabEntry( UdMnemonicCode.UD_Iroundpd, OpDefs.O_V, OpDefs.O_W, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1647 */ new UdItabEntry( UdMnemonicCode.UD_Ivroundpd, OpDefs.O_Vx, OpDefs.O_Wx, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb|BitOps.PVexl ),
            /* 1648 */ new UdItabEntry( UdMnemonicCode.UD_Iroundss, OpDefs.O_V, OpDefs.O_W, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1649 */ new UdItabEntry( UdMnemonicCode.UD_Ivroundss, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_Ib, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1650 */ new UdItabEntry( UdMnemonicCode.UD_Iroundsd, OpDefs.O_V, OpDefs.O_W, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1651 */ new UdItabEntry( UdMnemonicCode.UD_Ivroundsd, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_Ib, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1652 */ new UdItabEntry( UdMnemonicCode.UD_Iblendpd, OpDefs.O_V, OpDefs.O_W, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1653 */ new UdItabEntry( UdMnemonicCode.UD_Ivblendpd, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_Ib, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb|BitOps.PVexl ),
            /* 1654 */ new UdItabEntry( UdMnemonicCode.UD_Iblendps, OpDefs.O_V, OpDefs.O_W, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1655 */ new UdItabEntry( UdMnemonicCode.UD_Ivblendps, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_Ib, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1656 */ new UdItabEntry( UdMnemonicCode.UD_Iblendvpd, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1657 */ new UdItabEntry( UdMnemonicCode.UD_Iblendvps, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1658 */ new UdItabEntry( UdMnemonicCode.UD_Ibound, OpDefs.O_Gv, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso ),
            /* 1659 */ new UdItabEntry( UdMnemonicCode.UD_Ibsf, OpDefs.O_Gv, OpDefs.O_Ev, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1660 */ new UdItabEntry( UdMnemonicCode.UD_Ibsr, OpDefs.O_Gv, OpDefs.O_Ev, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1661 */ new UdItabEntry( UdMnemonicCode.UD_Ibswap, OpDefs.O_R0y, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso|BitOps.PRexw|BitOps.PRexb ),
            /* 1662 */ new UdItabEntry( UdMnemonicCode.UD_Ibswap, OpDefs.O_R1y, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso|BitOps.PRexw|BitOps.PRexb ),
            /* 1663 */ new UdItabEntry( UdMnemonicCode.UD_Ibswap, OpDefs.O_R2y, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso|BitOps.PRexw|BitOps.PRexb ),
            /* 1664 */ new UdItabEntry( UdMnemonicCode.UD_Ibswap, OpDefs.O_R3y, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso|BitOps.PRexw|BitOps.PRexb ),
            /* 1665 */ new UdItabEntry( UdMnemonicCode.UD_Ibswap, OpDefs.O_R4y, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso|BitOps.PRexw|BitOps.PRexb ),
            /* 1666 */ new UdItabEntry( UdMnemonicCode.UD_Ibswap, OpDefs.O_R5y, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso|BitOps.PRexw|BitOps.PRexb ),
            /* 1667 */ new UdItabEntry( UdMnemonicCode.UD_Ibswap, OpDefs.O_R6y, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso|BitOps.PRexw|BitOps.PRexb ),
            /* 1668 */ new UdItabEntry( UdMnemonicCode.UD_Ibswap, OpDefs.O_R7y, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.POso|BitOps.PRexw|BitOps.PRexb ),
            /* 1669 */ new UdItabEntry( UdMnemonicCode.UD_Ibt, OpDefs.O_Ev, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1670 */ new UdItabEntry( UdMnemonicCode.UD_Ibt, OpDefs.O_Ev, OpDefs.O_Gv, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1671 */ new UdItabEntry( UdMnemonicCode.UD_Ibtc, OpDefs.O_Ev, OpDefs.O_Gv, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1672 */ new UdItabEntry( UdMnemonicCode.UD_Ibtc, OpDefs.O_Ev, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1673 */ new UdItabEntry( UdMnemonicCode.UD_Ibtr, OpDefs.O_Ev, OpDefs.O_Gv, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1674 */ new UdItabEntry( UdMnemonicCode.UD_Ibtr, OpDefs.O_Ev, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1675 */ new UdItabEntry( UdMnemonicCode.UD_Ibts, OpDefs.O_Ev, OpDefs.O_Gv, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1676 */ new UdItabEntry( UdMnemonicCode.UD_Ibts, OpDefs.O_Ev, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexw|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1677 */ new UdItabEntry( UdMnemonicCode.UD_Ipblendw, OpDefs.O_V, OpDefs.O_W, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1678 */ new UdItabEntry( UdMnemonicCode.UD_Ivpblendw, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_Ib, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1679 */ new UdItabEntry( UdMnemonicCode.UD_Impsadbw, OpDefs.O_V, OpDefs.O_W, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1680 */ new UdItabEntry( UdMnemonicCode.UD_Ivmpsadbw, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_Ib, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb|BitOps.PVexl ),
            /* 1681 */ new UdItabEntry( UdMnemonicCode.UD_Imovntdqa, OpDefs.O_V, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexw|BitOps.PRexx|BitOps.PRexb ),
            /* 1682 */ new UdItabEntry( UdMnemonicCode.UD_Ivmovntdqa, OpDefs.O_Vx, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexw|BitOps.PRexx|BitOps.PRexb|BitOps.PVexl ),
            /* 1683 */ new UdItabEntry( UdMnemonicCode.UD_Ipackusdw, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexw|BitOps.PRexx|BitOps.PRexb ),
            /* 1684 */ new UdItabEntry( UdMnemonicCode.UD_Ivpackusdw, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexw|BitOps.PRexx|BitOps.PRexb|BitOps.PVexl ),
            /* 1685 */ new UdItabEntry( UdMnemonicCode.UD_Ipmovsxbw, OpDefs.O_V, OpDefs.O_MqU, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexw|BitOps.PRexx|BitOps.PRexb ),
            /* 1686 */ new UdItabEntry( UdMnemonicCode.UD_Ivpmovsxbw, OpDefs.O_Vx, OpDefs.O_MqU, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexw|BitOps.PRexx|BitOps.PRexb ),
            /* 1687 */ new UdItabEntry( UdMnemonicCode.UD_Ipmovsxbd, OpDefs.O_V, OpDefs.O_MdU, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexw|BitOps.PRexx|BitOps.PRexb ),
            /* 1688 */ new UdItabEntry( UdMnemonicCode.UD_Ivpmovsxbd, OpDefs.O_Vx, OpDefs.O_MdU, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexw|BitOps.PRexx|BitOps.PRexb ),
            /* 1689 */ new UdItabEntry( UdMnemonicCode.UD_Ipmovsxbq, OpDefs.O_V, OpDefs.O_MwU, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexw|BitOps.PRexx|BitOps.PRexb ),
            /* 1690 */ new UdItabEntry( UdMnemonicCode.UD_Ivpmovsxbq, OpDefs.O_Vx, OpDefs.O_MwU, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexw|BitOps.PRexx|BitOps.PRexb ),
            /* 1691 */ new UdItabEntry( UdMnemonicCode.UD_Ipmovsxwd, OpDefs.O_V, OpDefs.O_MqU, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexw|BitOps.PRexx|BitOps.PRexb ),
            /* 1692 */ new UdItabEntry( UdMnemonicCode.UD_Ivpmovsxwd, OpDefs.O_Vx, OpDefs.O_MqU, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexw|BitOps.PRexx|BitOps.PRexb ),
            /* 1693 */ new UdItabEntry( UdMnemonicCode.UD_Ipmovsxwq, OpDefs.O_V, OpDefs.O_MdU, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexw|BitOps.PRexx|BitOps.PRexb ),
            /* 1694 */ new UdItabEntry( UdMnemonicCode.UD_Ivpmovsxwq, OpDefs.O_Vx, OpDefs.O_MdU, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexw|BitOps.PRexx|BitOps.PRexb ),
            /* 1695 */ new UdItabEntry( UdMnemonicCode.UD_Ipmovsxdq, OpDefs.O_V, OpDefs.O_MqU, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexw|BitOps.PRexx|BitOps.PRexb ),
            /* 1696 */ new UdItabEntry( UdMnemonicCode.UD_Ipmovzxbw, OpDefs.O_V, OpDefs.O_MqU, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexw|BitOps.PRexx|BitOps.PRexb ),
            /* 1697 */ new UdItabEntry( UdMnemonicCode.UD_Ivpmovzxbw, OpDefs.O_Vx, OpDefs.O_MqU, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexw|BitOps.PRexx|BitOps.PRexb ),
            /* 1698 */ new UdItabEntry( UdMnemonicCode.UD_Ipmovzxbd, OpDefs.O_V, OpDefs.O_MdU, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexw|BitOps.PRexx|BitOps.PRexb ),
            /* 1699 */ new UdItabEntry( UdMnemonicCode.UD_Ivpmovzxbd, OpDefs.O_Vx, OpDefs.O_MdU, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexw|BitOps.PRexx|BitOps.PRexb ),
            /* 1700 */ new UdItabEntry( UdMnemonicCode.UD_Ipmovzxbq, OpDefs.O_V, OpDefs.O_MwU, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexw|BitOps.PRexx|BitOps.PRexb ),
            /* 1701 */ new UdItabEntry( UdMnemonicCode.UD_Ivpmovzxbq, OpDefs.O_Vx, OpDefs.O_MwU, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexw|BitOps.PRexx|BitOps.PRexb ),
            /* 1702 */ new UdItabEntry( UdMnemonicCode.UD_Ipmovzxwd, OpDefs.O_V, OpDefs.O_MqU, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexw|BitOps.PRexx|BitOps.PRexb ),
            /* 1703 */ new UdItabEntry( UdMnemonicCode.UD_Ivpmovzxwd, OpDefs.O_Vx, OpDefs.O_MqU, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexw|BitOps.PRexx|BitOps.PRexb ),
            /* 1704 */ new UdItabEntry( UdMnemonicCode.UD_Ipmovzxwq, OpDefs.O_V, OpDefs.O_MdU, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexw|BitOps.PRexx|BitOps.PRexb ),
            /* 1705 */ new UdItabEntry( UdMnemonicCode.UD_Ivpmovzxwq, OpDefs.O_Vx, OpDefs.O_MdU, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexw|BitOps.PRexx|BitOps.PRexb ),
            /* 1706 */ new UdItabEntry( UdMnemonicCode.UD_Ipmovzxdq, OpDefs.O_V, OpDefs.O_MqU, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexw|BitOps.PRexx|BitOps.PRexb ),
            /* 1707 */ new UdItabEntry( UdMnemonicCode.UD_Ivpmovzxdq, OpDefs.O_Vx, OpDefs.O_MqU, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexw|BitOps.PRexx|BitOps.PRexb ),
            /* 1708 */ new UdItabEntry( UdMnemonicCode.UD_Ipcmpeqq, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexw|BitOps.PRexx|BitOps.PRexb ),
            /* 1709 */ new UdItabEntry( UdMnemonicCode.UD_Ivpcmpeqq, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexw|BitOps.PRexx|BitOps.PRexb ),
            /* 1710 */ new UdItabEntry( UdMnemonicCode.UD_Ipopcnt, OpDefs.O_Gv, OpDefs.O_Ev, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexr|BitOps.PRexw|BitOps.PRexx|BitOps.PRexb ),
            /* 1711 */ new UdItabEntry( UdMnemonicCode.UD_Iptest, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexw|BitOps.PRexx|BitOps.PRexb ),
            /* 1712 */ new UdItabEntry( UdMnemonicCode.UD_Ivptest, OpDefs.O_Vx, OpDefs.O_Wx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexw|BitOps.PRexx|BitOps.PRexb|BitOps.PVexl ),
            /* 1713 */ new UdItabEntry( UdMnemonicCode.UD_Ipcmpestri, OpDefs.O_V, OpDefs.O_W, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexw|BitOps.PRexx|BitOps.PRexb ),
            /* 1714 */ new UdItabEntry( UdMnemonicCode.UD_Ivpcmpestri, OpDefs.O_Vx, OpDefs.O_Wx, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexw|BitOps.PRexx|BitOps.PRexb ),
            /* 1715 */ new UdItabEntry( UdMnemonicCode.UD_Ipcmpestrm, OpDefs.O_V, OpDefs.O_W, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexw|BitOps.PRexx|BitOps.PRexb ),
            /* 1716 */ new UdItabEntry( UdMnemonicCode.UD_Ivpcmpestrm, OpDefs.O_Vx, OpDefs.O_Wx, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexw|BitOps.PRexx|BitOps.PRexb ),
            /* 1717 */ new UdItabEntry( UdMnemonicCode.UD_Ipcmpgtq, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexw|BitOps.PRexx|BitOps.PRexb ),
            /* 1718 */ new UdItabEntry( UdMnemonicCode.UD_Ivpcmpgtq, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexw|BitOps.PRexx|BitOps.PRexb ),
            /* 1719 */ new UdItabEntry( UdMnemonicCode.UD_Ipcmpistri, OpDefs.O_V, OpDefs.O_W, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexw|BitOps.PRexx|BitOps.PRexb ),
            /* 1720 */ new UdItabEntry( UdMnemonicCode.UD_Ivpcmpistri, OpDefs.O_Vx, OpDefs.O_Wx, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexw|BitOps.PRexx|BitOps.PRexb ),
            /* 1721 */ new UdItabEntry( UdMnemonicCode.UD_Ipcmpistrm, OpDefs.O_V, OpDefs.O_W, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexw|BitOps.PRexx|BitOps.PRexb ),
            /* 1722 */ new UdItabEntry( UdMnemonicCode.UD_Ivpcmpistrm, OpDefs.O_Vx, OpDefs.O_Wx, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexw|BitOps.PRexx|BitOps.PRexb ),
            /* 1723 */ new UdItabEntry( UdMnemonicCode.UD_Imovbe, OpDefs.O_Gv, OpDefs.O_Mv, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexr|BitOps.PRexw|BitOps.PRexx|BitOps.PRexb ),
            /* 1724 */ new UdItabEntry( UdMnemonicCode.UD_Imovbe, OpDefs.O_Mv, OpDefs.O_Gv, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexr|BitOps.PRexw|BitOps.PRexx|BitOps.PRexb ),
            /* 1725 */ new UdItabEntry( UdMnemonicCode.UD_Icrc32, OpDefs.O_Gy, OpDefs.O_Eb, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexr|BitOps.PRexw|BitOps.PRexx|BitOps.PRexb ),
            /* 1726 */ new UdItabEntry( UdMnemonicCode.UD_Icrc32, OpDefs.O_Gy, OpDefs.O_Ev, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.POso|BitOps.PRexr|BitOps.PRexw|BitOps.PRexx|BitOps.PRexb ),
            /* 1727 */ new UdItabEntry( UdMnemonicCode.UD_Ivbroadcastss, OpDefs.O_V, OpDefs.O_Md, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb|BitOps.PVexl ),
            /* 1728 */ new UdItabEntry( UdMnemonicCode.UD_Ivbroadcastsd, OpDefs.O_Vqq, OpDefs.O_Mq, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb|BitOps.PVexl ),
            /* 1729 */ new UdItabEntry( UdMnemonicCode.UD_Ivextractf128, OpDefs.O_Wdq, OpDefs.O_Vqq, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb|BitOps.PVexl ),
            /* 1730 */ new UdItabEntry( UdMnemonicCode.UD_Ivinsertf128, OpDefs.O_Vqq, OpDefs.O_Hqq, OpDefs.O_Wdq, OpDefs.O_Ib, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb|BitOps.PVexl ),
            /* 1731 */ new UdItabEntry( UdMnemonicCode.UD_Ivmaskmovps, OpDefs.O_V, OpDefs.O_H, OpDefs.O_M, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb|BitOps.PVexl ),
            /* 1732 */ new UdItabEntry( UdMnemonicCode.UD_Ivmaskmovps, OpDefs.O_M, OpDefs.O_H, OpDefs.O_V, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb|BitOps.PVexl ),
            /* 1733 */ new UdItabEntry( UdMnemonicCode.UD_Ivmaskmovpd, OpDefs.O_V, OpDefs.O_H, OpDefs.O_M, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb|BitOps.PVexl ),
            /* 1734 */ new UdItabEntry( UdMnemonicCode.UD_Ivmaskmovpd, OpDefs.O_M, OpDefs.O_H, OpDefs.O_V, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb|BitOps.PVexl ),
            /* 1735 */ new UdItabEntry( UdMnemonicCode.UD_Ivpermilpd, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb|BitOps.PVexl ),
            /* 1736 */ new UdItabEntry( UdMnemonicCode.UD_Ivpermilpd, OpDefs.O_V, OpDefs.O_W, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb|BitOps.PVexl ),
            /* 1737 */ new UdItabEntry( UdMnemonicCode.UD_Ivpermilps, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb|BitOps.PVexl ),
            /* 1738 */ new UdItabEntry( UdMnemonicCode.UD_Ivpermilps, OpDefs.O_Vx, OpDefs.O_Wx, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb|BitOps.PVexl ),
            /* 1739 */ new UdItabEntry( UdMnemonicCode.UD_Ivperm2f128, OpDefs.O_Vqq, OpDefs.O_Hqq, OpDefs.O_Wqq, OpDefs.O_Ib, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb|BitOps.PVexl ),
            /* 1740 */ new UdItabEntry( UdMnemonicCode.UD_Ivtestps, OpDefs.O_Vx, OpDefs.O_Wx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb|BitOps.PVexl ),
            /* 1741 */ new UdItabEntry( UdMnemonicCode.UD_Ivtestpd, OpDefs.O_Vx, OpDefs.O_Wx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb|BitOps.PVexl ),
            /* 1742 */ new UdItabEntry( UdMnemonicCode.UD_Ivzeroupper, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 1743 */ new UdItabEntry( UdMnemonicCode.UD_Ivzeroall, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PNone ),
            /* 1744 */ new UdItabEntry( UdMnemonicCode.UD_Ivblendvpd, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_Lx, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb|BitOps.PVexl ),
            /* 1745 */ new UdItabEntry( UdMnemonicCode.UD_Ivblendvps, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_Lx, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb|BitOps.PVexl ),
            /* 1746 */ new UdItabEntry( UdMnemonicCode.UD_Ivmovsd, OpDefs.O_V, OpDefs.O_H, OpDefs.O_U, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1747 */ new UdItabEntry( UdMnemonicCode.UD_Ivmovsd, OpDefs.O_V, OpDefs.O_Mq, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1748 */ new UdItabEntry( UdMnemonicCode.UD_Ivmovsd, OpDefs.O_U, OpDefs.O_H, OpDefs.O_V, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1749 */ new UdItabEntry( UdMnemonicCode.UD_Ivmovsd, OpDefs.O_Mq, OpDefs.O_V, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1750 */ new UdItabEntry( UdMnemonicCode.UD_Ivmovss, OpDefs.O_V, OpDefs.O_H, OpDefs.O_U, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1751 */ new UdItabEntry( UdMnemonicCode.UD_Ivmovss, OpDefs.O_V, OpDefs.O_Md, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1752 */ new UdItabEntry( UdMnemonicCode.UD_Ivmovss, OpDefs.O_U, OpDefs.O_H, OpDefs.O_V, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1753 */ new UdItabEntry( UdMnemonicCode.UD_Ivmovss, OpDefs.O_Md, OpDefs.O_V, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1754 */ new UdItabEntry( UdMnemonicCode.UD_Ivpblendvb, OpDefs.O_V, OpDefs.O_H, OpDefs.O_W, OpDefs.O_L, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1755 */ new UdItabEntry( UdMnemonicCode.UD_Ivpsllw, OpDefs.O_V, OpDefs.O_H, OpDefs.O_W, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1756 */ new UdItabEntry( UdMnemonicCode.UD_Ivpsllw, OpDefs.O_H, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1757 */ new UdItabEntry( UdMnemonicCode.UD_Ivpslld, OpDefs.O_V, OpDefs.O_H, OpDefs.O_W, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1758 */ new UdItabEntry( UdMnemonicCode.UD_Ivpslld, OpDefs.O_H, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1759 */ new UdItabEntry( UdMnemonicCode.UD_Ivpsllq, OpDefs.O_V, OpDefs.O_H, OpDefs.O_W, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
            /* 1760 */ new UdItabEntry( UdMnemonicCode.UD_Ivpsllq, OpDefs.O_H, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, BitOps.PAso|BitOps.PRexr|BitOps.PRexx|BitOps.PRexb ),
        };


        internal static readonly string[] ud_mnemonics_str = new string[]
        {
            "aaa",
            "aad",
            "aam",
            "aas",
            "adc",
            "add",
            "addpd",
            "addps",
            "addsd",
            "addss",
            "addsubpd",
            "addsubps",
            "aesdec",
            "aesdeclast",
            "aesenc",
            "aesenclast",
            "aesimc",
            "aeskeygenassist",
            "and",
            "andnpd",
            "andnps",
            "andpd",
            "andps",
            "arpl",
            "blendpd",
            "blendps",
            "blendvpd",
            "blendvps",
            "bound",
            "bsf",
            "bsr",
            "bswap",
            "bt",
            "btc",
            "btr",
            "bts",
            "call",
            "cbw",
            "cdq",
            "cdqe",
            "clc",
            "cld",
            "clflush",
            "clgi",
            "cli",
            "clts",
            "cmc",
            "cmova",
            "cmovae",
            "cmovb",
            "cmovbe",
            "cmovg",
            "cmovge",
            "cmovl",
            "cmovle",
            "cmovno",
            "cmovnp",
            "cmovns",
            "cmovnz",
            "cmovo",
            "cmovp",
            "cmovs",
            "cmovz",
            "cmp",
            "cmppd",
            "cmpps",
            "cmpsb",
            "cmpsd",
            "cmpsq",
            "cmpss",
            "cmpsw",
            "cmpxchg",
            "cmpxchg16b",
            "cmpxchg8b",
            "comisd",
            "comiss",
            "cpuid",
            "cqo",
            "crc32",
            "cvtdq2pd",
            "cvtdq2ps",
            "cvtpd2dq",
            "cvtpd2pi",
            "cvtpd2ps",
            "cvtpi2pd",
            "cvtpi2ps",
            "cvtps2dq",
            "cvtps2pd",
            "cvtps2pi",
            "cvtsd2si",
            "cvtsd2ss",
            "cvtsi2sd",
            "cvtsi2ss",
            "cvtss2sd",
            "cvtss2si",
            "cvttpd2dq",
            "cvttpd2pi",
            "cvttps2dq",
            "cvttps2pi",
            "cvttsd2si",
            "cvttss2si",
            "cwd",
            "cwde",
            "daa",
            "das",
            "dec",
            "div",
            "divpd",
            "divps",
            "divsd",
            "divss",
            "dppd",
            "dpps",
            "emms",
            "enter",
            "extractps",
            "f2xm1",
            "fabs",
            "fadd",
            "faddp",
            "fbld",
            "fbstp",
            "fchs",
            "fclex",
            "fcmovb",
            "fcmovbe",
            "fcmove",
            "fcmovnb",
            "fcmovnbe",
            "fcmovne",
            "fcmovnu",
            "fcmovu",
            "fcom",
            "fcom2",
            "fcomi",
            "fcomip",
            "fcomp",
            "fcomp3",
            "fcomp5",
            "fcompp",
            "fcos",
            "fdecstp",
            "fdiv",
            "fdivp",
            "fdivr",
            "fdivrp",
            "femms",
            "ffree",
            "ffreep",
            "fiadd",
            "ficom",
            "ficomp",
            "fidiv",
            "fidivr",
            "fild",
            "fimul",
            "fincstp",
            "fist",
            "fistp",
            "fisttp",
            "fisub",
            "fisubr",
            "fld",
            "fld1",
            "fldcw",
            "fldenv",
            "fldl2e",
            "fldl2t",
            "fldlg2",
            "fldln2",
            "fldpi",
            "fldz",
            "fmul",
            "fmulp",
            "fndisi",
            "fneni",
            "fninit",
            "fnop",
            "fnsave",
            "fnsetpm",
            "fnstcw",
            "fnstenv",
            "fnstsw",
            "fpatan",
            "fprem",
            "fprem1",
            "fptan",
            "frndint",
            "frstor",
            "frstpm",
            "fscale",
            "fsin",
            "fsincos",
            "fsqrt",
            "fst",
            "fstp",
            "fstp1",
            "fstp8",
            "fstp9",
            "fsub",
            "fsubp",
            "fsubr",
            "fsubrp",
            "ftst",
            "fucom",
            "fucomi",
            "fucomip",
            "fucomp",
            "fucompp",
            "fxam",
            "fxch",
            "fxch4",
            "fxch7",
            "fxrstor",
            "fxsave",
            "fxtract",
            "fyl2x",
            "fyl2xp1",
            "getsec",
            "haddpd",
            "haddps",
            "hlt",
            "hsubpd",
            "hsubps",
            "idiv",
            "imul",
            "in",
            "inc",
            "insb",
            "insd",
            "insertps",
            "insw",
            "int",
            "int1",
            "int3",
            "into",
            "invd",
            "invept",
            "invlpg",
            "invlpga",
            "invvpid",
            "iretd",
            "iretq",
            "iretw",
            "ja",
            "jae",
            "jb",
            "jbe",
            "jcxz",
            "jecxz",
            "jg",
            "jge",
            "jl",
            "jle",
            "jmp",
            "jno",
            "jnp",
            "jns",
            "jnz",
            "jo",
            "jp",
            "jrcxz",
            "js",
            "jz",
            "lahf",
            "lar",
            "lddqu",
            "ldmxcsr",
            "lds",
            "lea",
            "leave",
            "les",
            "lfence",
            "lfs",
            "lgdt",
            "lgs",
            "lidt",
            "lldt",
            "lmsw",
            "lock",
            "lodsb",
            "lodsd",
            "lodsq",
            "lodsw",
            "loop",
            "loope",
            "loopne",
            "lsl",
            "lss",
            "ltr",
            "maskmovdqu",
            "maskmovq",
            "maxpd",
            "maxps",
            "maxsd",
            "maxss",
            "mfence",
            "minpd",
            "minps",
            "minsd",
            "minss",
            "monitor",
            "montmul",
            "mov",
            "movapd",
            "movaps",
            "movbe",
            "movd",
            "movddup",
            "movdq2q",
            "movdqa",
            "movdqu",
            "movhlps",
            "movhpd",
            "movhps",
            "movlhps",
            "movlpd",
            "movlps",
            "movmskpd",
            "movmskps",
            "movntdq",
            "movntdqa",
            "movnti",
            "movntpd",
            "movntps",
            "movntq",
            "movq",
            "movq2dq",
            "movsb",
            "movsd",
            "movshdup",
            "movsldup",
            "movsq",
            "movss",
            "movsw",
            "movsx",
            "movsxd",
            "movupd",
            "movups",
            "movzx",
            "mpsadbw",
            "mul",
            "mulpd",
            "mulps",
            "mulsd",
            "mulss",
            "mwait",
            "neg",
            "nop",
            "not",
            "or",
            "orpd",
            "orps",
            "out",
            "outsb",
            "outsd",
            "outsw",
            "pabsb",
            "pabsd",
            "pabsw",
            "packssdw",
            "packsswb",
            "packusdw",
            "packuswb",
            "paddb",
            "paddd",
            "paddq",
            "paddsb",
            "paddsw",
            "paddusb",
            "paddusw",
            "paddw",
            "palignr",
            "pand",
            "pandn",
            "pavgb",
            "pavgusb",
            "pavgw",
            "pblendvb",
            "pblendw",
            "pclmulqdq",
            "pcmpeqb",
            "pcmpeqd",
            "pcmpeqq",
            "pcmpeqw",
            "pcmpestri",
            "pcmpestrm",
            "pcmpgtb",
            "pcmpgtd",
            "pcmpgtq",
            "pcmpgtw",
            "pcmpistri",
            "pcmpistrm",
            "pextrb",
            "pextrd",
            "pextrq",
            "pextrw",
            "pf2id",
            "pf2iw",
            "pfacc",
            "pfadd",
            "pfcmpeq",
            "pfcmpge",
            "pfcmpgt",
            "pfmax",
            "pfmin",
            "pfmul",
            "pfnacc",
            "pfpnacc",
            "pfrcp",
            "pfrcpit1",
            "pfrcpit2",
            "pfrsqit1",
            "pfrsqrt",
            "pfsub",
            "pfsubr",
            "phaddd",
            "phaddsw",
            "phaddw",
            "phminposuw",
            "phsubd",
            "phsubsw",
            "phsubw",
            "pi2fd",
            "pi2fw",
            "pinsrb",
            "pinsrd",
            "pinsrq",
            "pinsrw",
            "pmaddubsw",
            "pmaddwd",
            "pmaxsb",
            "pmaxsd",
            "pmaxsw",
            "pmaxub",
            "pmaxud",
            "pmaxuw",
            "pminsb",
            "pminsd",
            "pminsw",
            "pminub",
            "pminud",
            "pminuw",
            "pmovmskb",
            "pmovsxbd",
            "pmovsxbq",
            "pmovsxbw",
            "pmovsxdq",
            "pmovsxwd",
            "pmovsxwq",
            "pmovzxbd",
            "pmovzxbq",
            "pmovzxbw",
            "pmovzxdq",
            "pmovzxwd",
            "pmovzxwq",
            "pmuldq",
            "pmulhrsw",
            "pmulhrw",
            "pmulhuw",
            "pmulhw",
            "pmulld",
            "pmullw",
            "pmuludq",
            "pop",
            "popa",
            "popad",
            "popcnt",
            "popfd",
            "popfq",
            "popfw",
            "por",
            "prefetch",
            "prefetchnta",
            "prefetcht0",
            "prefetcht1",
            "prefetcht2",
            "psadbw",
            "pshufb",
            "pshufd",
            "pshufhw",
            "pshuflw",
            "pshufw",
            "psignb",
            "psignd",
            "psignw",
            "pslld",
            "pslldq",
            "psllq",
            "psllw",
            "psrad",
            "psraw",
            "psrld",
            "psrldq",
            "psrlq",
            "psrlw",
            "psubb",
            "psubd",
            "psubq",
            "psubsb",
            "psubsw",
            "psubusb",
            "psubusw",
            "psubw",
            "pswapd",
            "ptest",
            "punpckhbw",
            "punpckhdq",
            "punpckhqdq",
            "punpckhwd",
            "punpcklbw",
            "punpckldq",
            "punpcklqdq",
            "punpcklwd",
            "push",
            "pusha",
            "pushad",
            "pushfd",
            "pushfq",
            "pushfw",
            "pxor",
            "rcl",
            "rcpps",
            "rcpss",
            "rcr",
            "rdmsr",
            "rdpmc",
            "rdrand",
            "rdtsc",
            "rdtscp",
            "rep",
            "repne",
            "ret",
            "retf",
            "rol",
            "ror",
            "roundpd",
            "roundps",
            "roundsd",
            "roundss",
            "rsm",
            "rsqrtps",
            "rsqrtss",
            "sahf",
            "salc",
            "sar",
            "sbb",
            "scasb",
            "scasd",
            "scasq",
            "scasw",
            "seta",
            "setae",
            "setb",
            "setbe",
            "setg",
            "setge",
            "setl",
            "setle",
            "setno",
            "setnp",
            "setns",
            "setnz",
            "seto",
            "setp",
            "sets",
            "setz",
            "sfence",
            "sgdt",
            "shl",
            "shld",
            "shr",
            "shrd",
            "shufpd",
            "shufps",
            "sidt",
            "skinit",
            "sldt",
            "smsw",
            "sqrtpd",
            "sqrtps",
            "sqrtsd",
            "sqrtss",
            "stc",
            "std",
            "stgi",
            "sti",
            "stmxcsr",
            "stosb",
            "stosd",
            "stosq",
            "stosw",
            "str",
            "sub",
            "subpd",
            "subps",
            "subsd",
            "subss",
            "swapgs",
            "syscall",
            "sysenter",
            "sysexit",
            "sysret",
            "test",
            "ucomisd",
            "ucomiss",
            "ud2",
            "unpckhpd",
            "unpckhps",
            "unpcklpd",
            "unpcklps",
            "vaddpd",
            "vaddps",
            "vaddsd",
            "vaddss",
            "vaddsubpd",
            "vaddsubps",
            "vaesdec",
            "vaesdeclast",
            "vaesenc",
            "vaesenclast",
            "vaesimc",
            "vaeskeygenassist",
            "vandnpd",
            "vandnps",
            "vandpd",
            "vandps",
            "vblendpd",
            "vblendps",
            "vblendvpd",
            "vblendvps",
            "vbroadcastsd",
            "vbroadcastss",
            "vcmppd",
            "vcmpps",
            "vcmpsd",
            "vcmpss",
            "vcomisd",
            "vcomiss",
            "vcvtdq2pd",
            "vcvtdq2ps",
            "vcvtpd2dq",
            "vcvtpd2ps",
            "vcvtps2dq",
            "vcvtps2pd",
            "vcvtsd2si",
            "vcvtsd2ss",
            "vcvtsi2sd",
            "vcvtsi2ss",
            "vcvtss2sd",
            "vcvtss2si",
            "vcvttpd2dq",
            "vcvttps2dq",
            "vcvttsd2si",
            "vcvttss2si",
            "vdivpd",
            "vdivps",
            "vdivsd",
            "vdivss",
            "vdppd",
            "vdpps",
            "verr",
            "verw",
            "vextractf128",
            "vextractps",
            "vhaddpd",
            "vhaddps",
            "vhsubpd",
            "vhsubps",
            "vinsertf128",
            "vinsertps",
            "vlddqu",
            "vmaskmovdqu",
            "vmaskmovpd",
            "vmaskmovps",
            "vmaxpd",
            "vmaxps",
            "vmaxsd",
            "vmaxss",
            "vmcall",
            "vmclear",
            "vminpd",
            "vminps",
            "vminsd",
            "vminss",
            "vmlaunch",
            "vmload",
            "vmmcall",
            "vmovapd",
            "vmovaps",
            "vmovd",
            "vmovddup",
            "vmovdqa",
            "vmovdqu",
            "vmovhlps",
            "vmovhpd",
            "vmovhps",
            "vmovlhps",
            "vmovlpd",
            "vmovlps",
            "vmovmskpd",
            "vmovmskps",
            "vmovntdq",
            "vmovntdqa",
            "vmovntpd",
            "vmovntps",
            "vmovq",
            "vmovsd",
            "vmovshdup",
            "vmovsldup",
            "vmovss",
            "vmovupd",
            "vmovups",
            "vmpsadbw",
            "vmptrld",
            "vmptrst",
            "vmread",
            "vmresume",
            "vmrun",
            "vmsave",
            "vmulpd",
            "vmulps",
            "vmulsd",
            "vmulss",
            "vmwrite",
            "vmxoff",
            "vmxon",
            "vorpd",
            "vorps",
            "vpabsb",
            "vpabsd",
            "vpabsw",
            "vpackssdw",
            "vpacksswb",
            "vpackusdw",
            "vpackuswb",
            "vpaddb",
            "vpaddd",
            "vpaddq",
            "vpaddsb",
            "vpaddsw",
            "vpaddusb",
            "vpaddusw",
            "vpaddw",
            "vpalignr",
            "vpand",
            "vpandn",
            "vpavgb",
            "vpavgw",
            "vpblendvb",
            "vpblendw",
            "vpclmulqdq",
            "vpcmpeqb",
            "vpcmpeqd",
            "vpcmpeqq",
            "vpcmpeqw",
            "vpcmpestri",
            "vpcmpestrm",
            "vpcmpgtb",
            "vpcmpgtd",
            "vpcmpgtq",
            "vpcmpgtw",
            "vpcmpistri",
            "vpcmpistrm",
            "vperm2f128",
            "vpermilpd",
            "vpermilps",
            "vpextrb",
            "vpextrd",
            "vpextrq",
            "vpextrw",
            "vphaddd",
            "vphaddsw",
            "vphaddw",
            "vphminposuw",
            "vphsubd",
            "vphsubsw",
            "vphsubw",
            "vpinsrb",
            "vpinsrd",
            "vpinsrq",
            "vpinsrw",
            "vpmaddubsw",
            "vpmaddwd",
            "vpmaxsb",
            "vpmaxsd",
            "vpmaxsw",
            "vpmaxub",
            "vpmaxud",
            "vpmaxuw",
            "vpminsb",
            "vpminsd",
            "vpminsw",
            "vpminub",
            "vpminud",
            "vpminuw",
            "vpmovmskb",
            "vpmovsxbd",
            "vpmovsxbq",
            "vpmovsxbw",
            "vpmovsxwd",
            "vpmovsxwq",
            "vpmovzxbd",
            "vpmovzxbq",
            "vpmovzxbw",
            "vpmovzxdq",
            "vpmovzxwd",
            "vpmovzxwq",
            "vpmuldq",
            "vpmulhrsw",
            "vpmulhuw",
            "vpmulhw",
            "vpmulld",
            "vpmullw",
            "vpor",
            "vpsadbw",
            "vpshufb",
            "vpshufd",
            "vpshufhw",
            "vpshuflw",
            "vpsignb",
            "vpsignd",
            "vpsignw",
            "vpslld",
            "vpslldq",
            "vpsllq",
            "vpsllw",
            "vpsrad",
            "vpsraw",
            "vpsrld",
            "vpsrldq",
            "vpsrlq",
            "vpsrlw",
            "vpsubb",
            "vpsubd",
            "vpsubq",
            "vpsubsb",
            "vpsubsw",
            "vpsubusb",
            "vpsubusw",
            "vpsubw",
            "vptest",
            "vpunpckhbw",
            "vpunpckhdq",
            "vpunpckhqdq",
            "vpunpckhwd",
            "vpunpcklbw",
            "vpunpckldq",
            "vpunpcklqdq",
            "vpunpcklwd",
            "vpxor",
            "vrcpps",
            "vrcpss",
            "vroundpd",
            "vroundps",
            "vroundsd",
            "vroundss",
            "vrsqrtps",
            "vrsqrtss",
            "vshufpd",
            "vshufps",
            "vsqrtpd",
            "vsqrtps",
            "vsqrtsd",
            "vsqrtss",
            "vstmxcsr",
            "vsubpd",
            "vsubps",
            "vsubsd",
            "vsubss",
            "vtestpd",
            "vtestps",
            "vucomisd",
            "vucomiss",
            "vunpckhpd",
            "vunpckhps",
            "vunpcklpd",
            "vunpcklps",
            "vxorpd",
            "vxorps",
            "vzeroall",
            "vzeroupper",
            "wait",
            "wbinvd",
            "wrmsr",
            "xadd",
            "xchg",
            "xcryptcbc",
            "xcryptcfb",
            "xcryptctr",
            "xcryptecb",
            "xcryptofb",
            "xgetbv",
            "xlatb",
            "xor",
            "xorpd",
            "xorps",
            "xrstor",
            "xsave",
            "xsetbv",
            "xsha1",
            "xsha256",
            "xstore",
            "invalid",
            "3dnow",
            "none",
            "db",
            "pause"
        };
        #endregion

    } // End class

} // End namespace