const std = @import("std");

pub fn isUnicodePunctuation(code: u21) bool {
    if (code < 128) {
        return @import("isPunctuation.zig").isPunctuation(@as(u8, @intCast(code)));
    }
    return code > 128 and ((code >= 161 and code <= 169) or
        (code >= 171 and code <= 172) or
        (code >= 174 and code <= 177) or
        code == 180 or
        (code >= 182 and code <= 184) or
        code == 187 or
        code == 191 or
        code == 215 or
        code == 247 or
        (code >= 706 and code <= 709) or
        (code >= 722 and code <= 735) or
        (code >= 741 and code <= 747) or
        code == 749 or
        (code >= 751 and code <= 767) or
        code == 885 or
        code == 894 or
        (code >= 900 and code <= 901) or
        code == 903 or
        code == 1014 or
        code == 1154 or
        (code >= 1370 and code <= 1375) or
        (code >= 1417 and code <= 1418) or
        (code >= 1421 and code <= 1423) or
        code == 1470 or
        code == 1472 or
        code == 1475 or
        code == 1478 or
        (code >= 1523 and code <= 1524) or
        (code >= 1542 and code <= 1551) or
        code == 1563 or
        (code >= 1565 and code <= 1567) or
        (code >= 1642 and code <= 1645) or
        code == 1748 or
        code == 1758 or
        code == 1769 or
        (code >= 1789 and code <= 1790) or
        (code >= 1792 and code <= 1805) or
        (code >= 2038 and code <= 2041) or
        (code >= 2046 and code <= 2047) or
        (code >= 2096 and code <= 2110) or
        code == 2142 or
        code == 2184 or
        (code >= 2404 and code <= 2405) or
        code == 2416 or
        (code >= 2546 and code <= 2547) or
        (code >= 2554 and code <= 2555) or
        code == 2557 or
        code == 2678 or
        (code >= 2800 and code <= 2801) or
        code == 2928 or
        (code >= 3059 and code <= 3066) or
        code == 3191 or
        code == 3199 or
        code == 3204 or
        code == 3407 or
        code == 3449 or
        code == 3572 or
        code == 3647 or
        code == 3663 or
        (code >= 3674 and code <= 3675) or
        (code >= 3841 and code <= 3863) or
        (code >= 3866 and code <= 3871) or
        code == 3892 or
        code == 3894 or
        code == 3896 or
        (code >= 3898 and code <= 3901) or
        code == 3973 or
        (code >= 4030 and code <= 4037) or
        (code >= 4039 and code <= 4044) or
        (code >= 4046 and code <= 4058) or
        (code >= 4170 and code <= 4175) or
        (code >= 4254 and code <= 4255) or
        code == 4347 or
        (code >= 4960 and code <= 4968) or
        (code >= 5008 and code <= 5017) or
        code == 5120 or
        (code >= 5741 and code <= 5742) or
        (code >= 5787 and code <= 5788) or
        (code >= 5867 and code <= 5869) or
        (code >= 5941 and code <= 5942) or
        (code >= 6100 and code <= 6102) or
        (code >= 6104 and code <= 6107) or
        (code >= 6144 and code <= 6154) or
        code == 6464 or
        (code >= 6468 and code <= 6469) or
        (code >= 6622 and code <= 6655) or
        (code >= 6686 and code <= 6687) or
        (code >= 6816 and code <= 6822) or
        (code >= 6824 and code <= 6829) or
        (code >= 7002 and code <= 7018) or
        (code >= 7028 and code <= 7038) or
        (code >= 7164 and code <= 7167) or
        (code >= 7227 and code <= 7231) or
        (code >= 7294 and code <= 7295) or
        (code >= 7360 and code <= 7367) or
        code == 7379 or
        code == 8125 or
        (code >= 8127 and code <= 8129) or
        (code >= 8141 and code <= 8143) or
        (code >= 8157 and code <= 8159) or
        (code >= 8173 and code <= 8175) or
        (code >= 8189 and code <= 8190) or
        (code >= 8208 and code <= 8231) or
        (code >= 8240 and code <= 8286) or
        (code >= 8314 and code <= 8318) or
        (code >= 8330 and code <= 8334) or
        (code >= 8352 and code <= 8384) or
        (code >= 8448 and code <= 8449) or
        (code >= 8451 and code <= 8454) or
        (code >= 8456 and code <= 8457) or
        code == 8468 or
        (code >= 8470 and code <= 8472) or
        (code >= 8478 and code <= 8483) or
        code == 8485 or
        code == 8487 or
        code == 8489 or
        code == 8494 or
        (code >= 8506 and code <= 8507) or
        (code >= 8512 and code <= 8516) or
        (code >= 8522 and code <= 8525) or
        code == 8527 or
        (code >= 8586 and code <= 8587) or
        (code >= 8592 and code <= 9254) or
        (code >= 9280 and code <= 9290) or
        (code >= 9372 and code <= 9449) or
        (code >= 9472 and code <= 10101) or
        (code >= 10132 and code <= 11123) or
        (code >= 11126 and code <= 11157) or
        (code >= 11159 and code <= 11263) or
        (code >= 11493 and code <= 11498) or
        (code >= 11513 and code <= 11516) or
        (code >= 11518 and code <= 11519) or
        code == 11632 or
        (code >= 11776 and code <= 11822) or
        (code >= 11824 and code <= 11869) or
        (code >= 11904 and code <= 11929) or
        (code >= 11931 and code <= 12019) or
        (code >= 12032 and code <= 12245) or
        (code >= 12272 and code <= 12283) or
        (code >= 12289 and code <= 12292) or
        (code >= 12296 and code <= 12320) or
        code == 12336 or
        (code >= 12342 and code <= 12343) or
        (code >= 12349 and code <= 12351) or
        (code >= 12443 and code <= 12444) or
        code == 12448 or
        code == 12539 or
        (code >= 12688 and code <= 12689) or
        (code >= 12694 and code <= 12703) or
        (code >= 12736 and code <= 12771) or
        (code >= 12800 and code <= 12830) or
        (code >= 12842 and code <= 12871) or
        code == 12880 or
        (code >= 12896 and code <= 12927) or
        (code >= 12938 and code <= 12976) or
        (code >= 12992 and code <= 13311) or
        (code >= 19904 and code <= 19967) or
        (code >= 42128 and code <= 42182) or
        (code >= 42238 and code <= 42239) or
        (code >= 42509 and code <= 42511) or
        code == 42611 or
        code == 42622 or
        (code >= 42738 and code <= 42743) or
        (code >= 42752 and code <= 42774) or
        (code >= 42784 and code <= 42785) or
        (code >= 42889 and code <= 42890) or
        (code >= 43048 and code <= 43051) or
        (code >= 43062 and code <= 43065) or
        (code >= 43124 and code <= 43127) or
        (code >= 43214 and code <= 43215) or
        (code >= 43256 and code <= 43258) or
        code == 43260 or
        (code >= 43310 and code <= 43311) or
        code == 43359 or
        (code >= 43457 and code <= 43469) or
        (code >= 43486 and code <= 43487) or
        (code >= 43612 and code <= 43615) or
        (code >= 43639 and code <= 43641) or
        (code >= 43742 and code <= 43743) or
        (code >= 43760 and code <= 43761) or
        code == 43867 or
        (code >= 43882 and code <= 43883) or
        code == 44011 or
        code == 64297 or
        (code >= 64434 and code <= 64450) or
        (code >= 64830 and code <= 64847) or
        code == 64975 or
        (code >= 65020 and code <= 65023) or
        (code >= 65040 and code <= 65049) or
        (code >= 65072 and code <= 65106) or
        (code >= 65108 and code <= 65126) or
        (code >= 65128 and code <= 65131) or
        (code >= 65281 and code <= 65295) or
        (code >= 65306 and code <= 65312) or
        (code >= 65339 and code <= 65344) or
        (code >= 65371 and code <= 65381) or
        (code >= 65504 and code <= 65510) or
        (code >= 65512 and code <= 65518) or
        (code >= 65532 and code <= 65533) or
        (code >= 65792 and code <= 65794) or
        (code >= 65847 and code <= 65855) or
        (code >= 65913 and code <= 65929) or
        (code >= 65932 and code <= 65934) or
        (code >= 65936 and code <= 65948) or
        code == 65952 or
        (code >= 66000 and code <= 66044) or
        code == 66463 or
        code == 66512 or
        code == 66927 or
        code == 67671 or
        (code >= 67703 and code <= 67704) or
        code == 67871 or
        code == 67903 or
        (code >= 68176 and code <= 68184) or
        code == 68223 or
        code == 68296 or
        (code >= 68336 and code <= 68342) or
        (code >= 68409 and code <= 68415) or
        (code >= 68505 and code <= 68508) or
        code == 69293 or
        (code >= 69461 and code <= 69465) or
        (code >= 69510 and code <= 69513) or
        (code >= 69703 and code <= 69709) or
        (code >= 69819 and code <= 69820) or
        (code >= 69822 and code <= 69825) or
        (code >= 69952 and code <= 69955) or
        (code >= 70004 and code <= 70005) or
        (code >= 70085 and code <= 70088) or
        code == 70093 or
        code == 70107 or
        (code >= 70109 and code <= 70111) or
        (code >= 70200 and code <= 70205) or
        code == 70313 or
        (code >= 70731 and code <= 70735) or
        (code >= 70746 and code <= 70747) or
        code == 70749 or
        code == 70854 or
        (code >= 71105 and code <= 71127) or
        (code >= 71233 and code <= 71235) or
        (code >= 71264 and code <= 71276) or
        code == 71353 or
        (code >= 71484 and code <= 71487) or
        code == 71739 or
        (code >= 72004 and code <= 72006) or
        code == 72162 or
        (code >= 72255 and code <= 72262) or
        (code >= 72346 and code <= 72348) or
        (code >= 72350 and code <= 72354) or
        (code >= 72448 and code <= 72457) or
        (code >= 72769 and code <= 72773) or
        (code >= 72816 and code <= 72817) or
        (code >= 73463 and code <= 73464) or
        (code >= 73539 and code <= 73551) or
        (code >= 73685 and code <= 73713) or
        code == 73727 or
        (code >= 74864 and code <= 74868) or
        (code >= 77809 and code <= 77810) or
        (code >= 92782 and code <= 92783) or
        code == 92917 or
        (code >= 92983 and code <= 92991) or
        (code >= 92996 and code <= 92997) or
        (code >= 93847 and code <= 93850) or
        code == 94178 or
        code == 113820 or
        code == 113823 or
        (code >= 118608 and code <= 118723) or
        (code >= 118784 and code <= 119029) or
        (code >= 119040 and code <= 119078) or
        (code >= 119081 and code <= 119140) or
        (code >= 119146 and code <= 119148) or
        (code >= 119171 and code <= 119172) or
        (code >= 119180 and code <= 119209) or
        (code >= 119214 and code <= 119274) or
        (code >= 119296 and code <= 119361) or
        code == 119365 or
        (code >= 119552 and code <= 119638) or
        code == 120513 or
        code == 120539 or
        code == 120571 or
        code == 120597 or
        code == 120629 or
        code == 120655 or
        code == 120687 or
        code == 120713 or
        code == 120745 or
        code == 120771 or
        (code >= 120832 and code <= 121343) or
        (code >= 121399 and code <= 121402) or
        (code >= 121453 and code <= 121460) or
        (code >= 121462 and code <= 121475) or
        (code >= 121477 and code <= 121483) or
        code == 123215 or
        code == 123647 or
        (code >= 125278 and code <= 125279) or
        code == 126124 or
        code == 126128 or
        code == 126254 or
        (code >= 126704 and code <= 126705) or
        (code >= 126976 and code <= 127019) or
        (code >= 127024 and code <= 127123) or
        (code >= 127136 and code <= 127150) or
        (code >= 127153 and code <= 127167) or
        (code >= 127169 and code <= 127183) or
        (code >= 127185 and code <= 127221) or
        (code >= 127245 and code <= 127405) or
        (code >= 127462 and code <= 127490) or
        (code >= 127504 and code <= 127547) or
        (code >= 127552 and code <= 127560) or
        (code >= 127568 and code <= 127569) or
        (code >= 127584 and code <= 127589) or
        (code >= 127744 and code <= 128727) or
        (code >= 128732 and code <= 128748) or
        (code >= 128752 and code <= 128764) or
        (code >= 128768 and code <= 128886) or
        (code >= 128891 and code <= 128985) or
        (code >= 128992 and code <= 129003) or
        code == 129008 or
        (code >= 129024 and code <= 129035) or
        (code >= 129040 and code <= 129095) or
        (code >= 129104 and code <= 129113) or
        (code >= 129120 and code <= 129159) or
        (code >= 129168 and code <= 129197) or
        (code >= 129200 and code <= 129201) or
        (code >= 129280 and code <= 129619) or
        (code >= 129632 and code <= 129645) or
        (code >= 129648 and code <= 129660) or
        (code >= 129664 and code <= 129672) or
        (code >= 129680 and code <= 129725) or
        (code >= 129727 and code <= 129733) or
        (code >= 129742 and code <= 129755) or
        (code >= 129760 and code <= 129768) or
        (code >= 129776 and code <= 129784) or
        (code >= 129792 and code <= 129938) or
        (code >= 129940 and code <= 129994));
}
