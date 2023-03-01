﻿using Core.Entities;
using Core.Interfaces.Data.Repositories;
using Moq;

namespace TestHelpers.MappingTables
{
    public class GenerateDistanceType
    {
        public static List<DistanceType> GenerateDistanceTypes() 
        {
            return new List<DistanceType>()
            {
                new DistanceType() {
                distance_type_id = 1, distance_type = "3m2f34y"
                },
                new DistanceType() {
                distance_type_id = 2, distance_type = "2m1f27y"
                },
                new DistanceType() {
                distance_type_id = 3, distance_type = "2m3f166y"
                },
                new DistanceType() {
                distance_type_id = 4, distance_type = "2m1f110y"
                },
                new DistanceType() {
                distance_type_id = 5, distance_type = "2m5f200y"
                },
                new DistanceType() {
                distance_type_id = 6, distance_type = "5f21y"
                },
                new DistanceType() {
                distance_type_id = 7, distance_type = "1m3f137y"
                },
                new DistanceType() {
                distance_type_id = 8, distance_type = "2m190y"
                },
                new DistanceType() {
                distance_type_id = 9, distance_type = "5f110y"
                },
                new DistanceType() {
                distance_type_id = 10, distance_type = "2m90y"
                },
                new DistanceType() {
                distance_type_id = 11, distance_type = "2m7f174y"
                },
                new DistanceType() {
                distance_type_id = 12, distance_type = "1m5f218y"
                },
                new DistanceType() {
                distance_type_id = 13, distance_type = "1m7f209y"
                },
                new DistanceType() {
                distance_type_id = 14, distance_type = "2m3f104y"
                },
                new DistanceType() {
                distance_type_id = 15, distance_type = "1m73y"
                },
                new DistanceType() {
                distance_type_id = 16, distance_type = "2m59y"
                },
                new DistanceType() {
                distance_type_id = 17, distance_type = "3m1f"
                },
                new DistanceType() {
                distance_type_id = 18, distance_type = "2m160y"
                },
                new DistanceType() {
                distance_type_id = 19, distance_type = "3m2f105y"
                },
                new DistanceType() {
                distance_type_id = 20, distance_type = "5f15y"
                },
                new DistanceType() {
                distance_type_id = 21, distance_type = "2m3f189y"
                },
                new DistanceType() {
                distance_type_id = 22, distance_type = "2m2f148y"
                },
                new DistanceType() {
                distance_type_id = 23, distance_type = "3m2f202y"
                },
                new DistanceType() {
                distance_type_id = 24, distance_type = "1m208y"
                },
                new DistanceType() {
                distance_type_id = 25, distance_type = "2m2f165y"
                },
                new DistanceType() {
                distance_type_id = 26, distance_type = "1m110y"
                },
                new DistanceType() {
                distance_type_id = 27, distance_type = "5f3y"
                },
                new DistanceType() {
                distance_type_id = 28, distance_type = "2m75y"
                },
                new DistanceType() {
                distance_type_id = 29, distance_type = "1m2f56y"
                },
                new DistanceType() {
                distance_type_id = 30, distance_type = "2m60y"
                },
                new DistanceType() {
                distance_type_id = 31, distance_type = "1m5f61y"
                },
                new DistanceType() {
                distance_type_id = 32, distance_type = "1m2f43y"
                },
                new DistanceType() {
                distance_type_id = 33, distance_type = "2m5f56y"
                },
                new DistanceType() {
                distance_type_id = 34, distance_type = "1m7f171y"
                },
                new DistanceType() {
                distance_type_id = 35, distance_type = "2m3f147y"
                },
                new DistanceType() {
                distance_type_id = 36, distance_type = "3m1f152y"
                },
                new DistanceType() {
                distance_type_id = 37, distance_type = "7f3y"
                },
                new DistanceType() {
                distance_type_id = 38, distance_type = "2m6f111y"
                },
                new DistanceType() {
                distance_type_id = 39, distance_type = "1m2f23y"
                },
                new DistanceType() {
                distance_type_id = 40, distance_type = "1m7f214y"
                },
                new DistanceType() {
                distance_type_id = 41, distance_type = "2m2f111y"
                },
                new DistanceType() {
                distance_type_id = 42, distance_type = "2m5f89y"
                },
                new DistanceType() {
                distance_type_id = 43, distance_type = "2m4f55y"
                },
                new DistanceType() {
                distance_type_id = 44, distance_type = "3m5y"
                },
                new DistanceType() {
                distance_type_id = 45, distance_type = "2m4f15y"
                },
                new DistanceType() {
                distance_type_id = 46, distance_type = "3m2f39y"
                },
                new DistanceType() {
                distance_type_id = 47, distance_type = "2m3f200y"
                },
                new DistanceType() {
                distance_type_id = 48, distance_type = "7f96y"
                },
                new DistanceType() {
                distance_type_id = 49, distance_type = "7f6y"
                },
                new DistanceType() {
                distance_type_id = 50, distance_type = "1m1f209y"
                },
                new DistanceType() {
                distance_type_id = 51, distance_type = "2m1f"
                },
                new DistanceType() {
                distance_type_id = 52, distance_type = "2m4f72y"
                },
                new DistanceType() {
                distance_type_id = 53, distance_type = "2m7f129y"
                },
                new DistanceType() {
                distance_type_id = 54, distance_type = "2m209y"
                },
                new DistanceType() {
                distance_type_id = 55, distance_type = "1m7f216y"
                },
                new DistanceType() {
                distance_type_id = 56, distance_type = "6f213y"
                },
                new DistanceType() {
                distance_type_id = 57, distance_type = "2m178y"
                },
                new DistanceType() {
                distance_type_id = 58, distance_type = "5f42y"
                },
                new DistanceType() {
                distance_type_id = 59, distance_type = "1m142y"
                },
                new DistanceType() {
                distance_type_id = 60, distance_type = "1m5f84y"
                },
                new DistanceType() {
                distance_type_id = 61, distance_type = "2m3f100y"
                },
                new DistanceType() {
                distance_type_id = 62, distance_type = "2m3f154y"
                },
                new DistanceType() {
                distance_type_id = 63, distance_type = "7f200y"
                },
                new DistanceType() {
                distance_type_id = 64, distance_type = "2m104y"
                },
                new DistanceType() {
                distance_type_id = 65, distance_type = "3m1f119y"
                },
                new DistanceType() {
                distance_type_id = 66, distance_type = "2m51y"
                },
                new DistanceType() {
                distance_type_id = 67, distance_type = "2m3f1y"
                },
                new DistanceType() {
                distance_type_id = 68, distance_type = "2m6f191y"
                },
                new DistanceType() {
                distance_type_id = 69, distance_type = "6f18y"
                },
                new DistanceType() {
                distance_type_id = 70, distance_type = "1m30y"
                },
                new DistanceType() {
                distance_type_id = 71, distance_type = "1m7f218y"
                },
                new DistanceType() {
                distance_type_id = 72, distance_type = "2m6f180y"
                },
                new DistanceType() {
                distance_type_id = 73, distance_type = "5f"
                },
                new DistanceType() {
                distance_type_id = 74, distance_type = "2m11y"
                },
                new DistanceType() {
                distance_type_id = 75, distance_type = "5f1y"
                },
                new DistanceType() {
                distance_type_id = 76, distance_type = "1m7f212y"
                },
                new DistanceType() {
                distance_type_id = 77, distance_type = "1m7f110y"
                },
                new DistanceType() {
                distance_type_id = 78, distance_type = "3m50y"
                },
                new DistanceType() {
                distance_type_id = 79, distance_type = "1m11y"
                },
                new DistanceType() {
                distance_type_id = 80, distance_type = "2m5f100y"
                },
                new DistanceType() {
                distance_type_id = 81, distance_type = "2m25y"
                },
                new DistanceType() {
                distance_type_id = 82, distance_type = "6f2y"
                },
                new DistanceType() {
                distance_type_id = 83, distance_type = "2m7f25y"
                },
                new DistanceType() {
                distance_type_id = 84, distance_type = "3m1f170y"
                },
                new DistanceType() {
                distance_type_id = 85, distance_type = "2m69y"
                },
                new DistanceType() {
                distance_type_id = 86, distance_type = "5f7y"
                },
                new DistanceType() {
                distance_type_id = 87, distance_type = "3m38y"
                },
                new DistanceType() {
                distance_type_id = 88, distance_type = "2m4f"
                },
                new DistanceType() {
                distance_type_id = 89, distance_type = "1m7f152y"
                },
                new DistanceType() {
                distance_type_id = 90, distance_type = "1m37y"
                },
                new DistanceType() {
                distance_type_id = 91, distance_type = "7f127y"
                },
                new DistanceType() {
                distance_type_id = 92, distance_type = "2m1f109y"
                },
                new DistanceType() {
                distance_type_id = 93, distance_type = "3m1f210y"
                },
                new DistanceType() {
                distance_type_id = 94, distance_type = "1m53y"
                },
                new DistanceType() {
                distance_type_id = 95, distance_type = "2m4f139y"
                },
                new DistanceType() {
                distance_type_id = 96, distance_type = "2m4f205y"
                },
                new DistanceType() {
                distance_type_id = 97, distance_type = "2m125y"
                },
                new DistanceType() {
                distance_type_id = 98, distance_type = "3m1f125y"
                },
                new DistanceType() {
                distance_type_id = 99, distance_type = "2m6f7y"
                },
                new DistanceType() {
                distance_type_id = 100, distance_type = "7f195y"
                },
                new DistanceType() {
                distance_type_id = 101, distance_type = "3m2f54y"
                },
                new DistanceType() {
                distance_type_id = 102, distance_type = "2m48y"
                },
                new DistanceType() {
                distance_type_id = 103, distance_type = "5f60y"
                },
                new DistanceType() {
                distance_type_id = 104, distance_type = "2m5f97y"
                },
                new DistanceType() {
                distance_type_id = 105, distance_type = "3m1f44y"
                },
                new DistanceType() {
                distance_type_id = 106, distance_type = "2m5f150y"
                },
                new DistanceType() {
                distance_type_id = 107, distance_type = "1m3f197y"
                },
                new DistanceType() {
                distance_type_id = 108, distance_type = "1m4f13y"
                },
                new DistanceType() {
                distance_type_id = 109, distance_type = "1m1y"
                },
                new DistanceType() {
                distance_type_id = 110, distance_type = "2m214y"
                },
                new DistanceType() {
                distance_type_id = 111, distance_type = "2m"
                },
                new DistanceType() {
                distance_type_id = 112, distance_type = "2m1f164y"
                },
                new DistanceType() {
                distance_type_id = 113, distance_type = "2m3y"
                },
                new DistanceType() {
                distance_type_id = 114, distance_type = "1m4f110y"
                },
                new DistanceType() {
                distance_type_id = 115, distance_type = "5f215y"
                },
                new DistanceType() {
                distance_type_id = 116, distance_type = "5f6y"
                },
                new DistanceType() {
                distance_type_id = 117, distance_type = "1m1f165y"
                },
                new DistanceType() {
                distance_type_id = 118, distance_type = "2m70y"
                },
                new DistanceType() {
                distance_type_id = 119, distance_type = "2m7f200y"
                },
                new DistanceType() {
                distance_type_id = 120, distance_type = "5f89y"
                },
                new DistanceType() {
                distance_type_id = 121, distance_type = "2m1f33y"
                },
                new DistanceType() {
                distance_type_id = 122, distance_type = "1m3f50y"
                },
                new DistanceType() {
                distance_type_id = 123, distance_type = "1m4f132y"
                },
                new DistanceType() {
                distance_type_id = 124, distance_type = "2m2f175y"
                },
                new DistanceType() {
                distance_type_id = 125, distance_type = "2m7f7y"
                },
                new DistanceType() {
                distance_type_id = 126, distance_type = "2m3f137y"
                },
                new DistanceType() {
                distance_type_id = 127, distance_type = "2m2f25y"
                },
                new DistanceType() {
                distance_type_id = 128, distance_type = "5f160y"
                },
                new DistanceType() {
                distance_type_id = 129, distance_type = "2m7f96y"
                },
                new DistanceType() {
                distance_type_id = 130, distance_type = "1m1f212y"
                },
                new DistanceType() {
                distance_type_id = 131, distance_type = "2m4f80y"
                },
                new DistanceType() {
                distance_type_id = 132, distance_type = "5f10y"
                },
                new DistanceType() {
                distance_type_id = 133, distance_type = "1m2f"
                },
                new DistanceType() {
                distance_type_id = 134, distance_type = "2m7f171y"
                },
                new DistanceType() {
                distance_type_id = 135, distance_type = "2m3f188y"
                },
                new DistanceType() {
                distance_type_id = 136, distance_type = "1m7f50y"
                },
                new DistanceType() {
                distance_type_id = 137, distance_type = "3m123y"
                },
                new DistanceType() {
                distance_type_id = 138, distance_type = "1m75y"
                },
                new DistanceType() {
                distance_type_id = 139, distance_type = "3m1f10y"
                },
                new DistanceType() {
                distance_type_id = 140, distance_type = "6f"
                },
                new DistanceType() {
                distance_type_id = 141, distance_type = "2m167y"
                },
                new DistanceType() {
                distance_type_id = 142, distance_type = "2m4f114y"
                },
                new DistanceType() {
                distance_type_id = 143, distance_type = "1m5f148y"
                },
                new DistanceType() {
                distance_type_id = 144, distance_type = "7f14y"
                },
                new DistanceType() {
                distance_type_id = 145, distance_type = "6f17y"
                },
                new DistanceType() {
                distance_type_id = 146, distance_type = "2m5f19y"
                },
                new DistanceType() {
                distance_type_id = 147, distance_type = "1m4f6y"
                },
                new DistanceType() {
                distance_type_id = 148, distance_type = "7f176y"
                },
                new DistanceType() {
                distance_type_id = 149, distance_type = "1m16y"
                },
                new DistanceType() {
                distance_type_id = 150, distance_type = "1m6f115y"
                },
                new DistanceType() {
                distance_type_id = 151, distance_type = "1m7f89y"
                },
                new DistanceType() {
                distance_type_id = 152, distance_type = "3m3f110y"
                },
                new DistanceType() {
                distance_type_id = 153, distance_type = "2m2f2y"
                },
                new DistanceType() {
                distance_type_id = 154, distance_type = "1m113y"
                },
                new DistanceType() {
                distance_type_id = 155, distance_type = "2m6f100y"
                },
                new DistanceType() {
                distance_type_id = 156, distance_type = "1m6f17y"
                },
                new DistanceType() {
                distance_type_id = 157, distance_type = "3m80y"
                },
                new DistanceType() {
                distance_type_id = 158, distance_type = "2m6f93y"
                },
                new DistanceType() {
                distance_type_id = 159, distance_type = "1m3f188y"
                },
                new DistanceType() {
                distance_type_id = 160, distance_type = "1m3f179y"
                },
                new DistanceType() {
                distance_type_id = 161, distance_type = "1m2f100y"
                },
                new DistanceType() {
                distance_type_id = 162, distance_type = "1m5f188y"
                },
                new DistanceType() {
                distance_type_id = 163, distance_type = "2m5f135y"
                },
                new DistanceType() {
                distance_type_id = 164, distance_type = "1m5f"
                },
                new DistanceType() {
                distance_type_id = 165, distance_type = "2m7f70y"
                },
                new DistanceType() {
                distance_type_id = 166, distance_type = "2m58y"
                },
                new DistanceType() {
                distance_type_id = 167, distance_type = "3m30y"
                },
                new DistanceType() {
                distance_type_id = 168, distance_type = "1m7f195y"
                },
                new DistanceType() {
                distance_type_id = 169, distance_type = "2m54y"
                },
                new DistanceType() {
                distance_type_id = 170, distance_type = "2m3f48y"
                },
                new DistanceType() {
                distance_type_id = 171, distance_type = "2m3f173y"
                },
                new DistanceType() {
                distance_type_id = 172, distance_type = "1m7f199y"
                },
                new DistanceType() {
                distance_type_id = 173, distance_type = "6f6y"
                },
                new DistanceType() {
                distance_type_id = 174, distance_type = "1m2f17y"
                },
                new DistanceType() {
                distance_type_id = 175, distance_type = "2m3f34y"
                },
                new DistanceType() {
                distance_type_id = 176, distance_type = "7f216y"
                },
                new DistanceType() {
                distance_type_id = 177, distance_type = "1m5f38y"
                },
                new DistanceType() {
                distance_type_id = 178, distance_type = "1m1f11y"
                },
                new DistanceType() {
                distance_type_id = 179, distance_type = "6f1y"
                },
                new DistanceType() {
                distance_type_id = 180, distance_type = "3m1f150y"
                },
                new DistanceType() {
                distance_type_id = 181, distance_type = "2m3f110y"
                },
                new DistanceType() {
                distance_type_id = 182, distance_type = "2m3f49y"
                },
                new DistanceType() {
                distance_type_id = 183, distance_type = "2m7f110y"
                },
                new DistanceType() {
                distance_type_id = 184, distance_type = "7f110y"
                },
                new DistanceType() {
                distance_type_id = 185, distance_type = "1m4f87y"
                },
                new DistanceType() {
                distance_type_id = 186, distance_type = "2m1f165y"
                },
                new DistanceType() {
                distance_type_id = 187, distance_type = "1m3f198y"
                },
                new DistanceType() {
                distance_type_id = 188, distance_type = "5f205y"
                },
                new DistanceType() {
                distance_type_id = 189, distance_type = "2m1f43y"
                },
                new DistanceType() {
                distance_type_id = 190, distance_type = "3m4f"
                },
                new DistanceType() {
                distance_type_id = 191, distance_type = "2m3f187y"
                },
                new DistanceType() {
                distance_type_id = 192, distance_type = "1m1f"
                },
                new DistanceType() {
                distance_type_id = 193, distance_type = "7f"
                },
                new DistanceType() {
                distance_type_id = 194, distance_type = "1m1f100y"
                },
                new DistanceType() {
                distance_type_id = 195, distance_type = "1m7f169y"
                },
                new DistanceType() {
                distance_type_id = 196, distance_type = "1m3f211y"
                },
                new DistanceType() {
                distance_type_id = 197, distance_type = "1m50y"
                },
                new DistanceType() {
                distance_type_id = 198, distance_type = "1m7f168y"
                },
                new DistanceType() {
                distance_type_id = 199, distance_type = "1m4f15y"
                },
                new DistanceType() {
                distance_type_id = 200, distance_type = "2m100y"
                },
                new DistanceType() {
                distance_type_id = 201, distance_type = "1m4f51y"
                },
                new DistanceType() {
                distance_type_id = 202, distance_type = "2m4f35y"
                },
                new DistanceType() {
                distance_type_id = 203, distance_type = "1m7f"
                },
                new DistanceType() {
                distance_type_id = 204, distance_type = "2m4f100y"
                },
                new DistanceType() {
                distance_type_id = 205, distance_type = "2m5f170y"
                },
                new DistanceType() {
                distance_type_id = 206, distance_type = "2m7f63y"
                },
                new DistanceType() {
                distance_type_id = 207, distance_type = "1m7f176y"
                },
                new DistanceType() {
                distance_type_id = 208, distance_type = "1m100y"
                },
                new DistanceType() {
                distance_type_id = 209, distance_type = "2m4f150y"
                },
                new DistanceType() {
                distance_type_id = 210, distance_type = "3m217y"
                },
                new DistanceType() {
                distance_type_id = 211, distance_type = "1m1f197y"
                },
                new DistanceType() {
                distance_type_id = 212, distance_type = "1m6f1y"
                },
                new DistanceType() {
                distance_type_id = 213, distance_type = "1m4f10y"
                },
                new DistanceType() {
                distance_type_id = 214, distance_type = "3m5f48y"
                },
                new DistanceType() {
                distance_type_id = 215, distance_type = "7f80y"
                },
                new DistanceType() {
                distance_type_id = 216, distance_type = "1m7f124y"
                },
                new DistanceType() {
                distance_type_id = 217, distance_type = "2m46y"
                },
                new DistanceType() {
                distance_type_id = 218, distance_type = "1m2f37y"
                },
                new DistanceType() {
                distance_type_id = 219, distance_type = "6f3y"
                },
                new DistanceType() {
                distance_type_id = 220, distance_type = "2m3f123y"
                },
                new DistanceType() {
                distance_type_id = 221, distance_type = "1m1f110y"
                },
                new DistanceType() {
                distance_type_id = 222, distance_type = "2m2f54y"
                },
                new DistanceType() {
                distance_type_id = 223, distance_type = "1m1f201y"
                },
                new DistanceType() {
                distance_type_id = 224, distance_type = "2m87y"
                },
                new DistanceType() {
                distance_type_id = 225, distance_type = "5f217y"
                },
                new DistanceType() {
                distance_type_id = 226, distance_type = "1m4f5y"
                },
                new DistanceType() {
                distance_type_id = 227, distance_type = "6f210y"
                },
                new DistanceType() {
                distance_type_id = 228, distance_type = "4m2f110y"
                },
                new DistanceType() {
                distance_type_id = 229, distance_type = "2m3f65y"
                },
                new DistanceType() {
                distance_type_id = 230, distance_type = "2m4f73y"
                },
                new DistanceType() {
                distance_type_id = 231, distance_type = "2m5f110y"
                },
                new DistanceType() {
                distance_type_id = 232, distance_type = "2m4f194y"
                },
                new DistanceType() {
                distance_type_id = 233, distance_type = "1m3f"
                },
                new DistanceType() {
                distance_type_id = 234, distance_type = "2m5f"
                },
                new DistanceType() {
                distance_type_id = 235, distance_type = "2m4f68y"
                },
                new DistanceType() {
                distance_type_id = 236, distance_type = "2m5f82y"
                },
                new DistanceType() {
                distance_type_id = 237, distance_type = "3m70y"
                },
                new DistanceType() {
                distance_type_id = 238, distance_type = "2m8y"
                },
                new DistanceType() {
                distance_type_id = 239, distance_type = "6f20y"
                },
                new DistanceType() {
                distance_type_id = 240, distance_type = "2m4f10y"
                },
                new DistanceType() {
                distance_type_id = 241, distance_type = "1m4f"
                },
                new DistanceType() {
                distance_type_id = 242, distance_type = "2m145y"
                },
                new DistanceType() {
                distance_type_id = 243, distance_type = "1m68y"
                },
                new DistanceType() {
                distance_type_id = 244, distance_type = "3m1f100y"
                },
                new DistanceType() {
                distance_type_id = 245, distance_type = "1m2f150y"
                },
                new DistanceType() {
                distance_type_id = 246, distance_type = "2m6f"
                },
                new DistanceType() {
                distance_type_id = 247, distance_type = "2m5f44y"
                },
                new DistanceType() {
                distance_type_id = 248, distance_type = "1m4f98y"
                },
                new DistanceType() {
                distance_type_id = 249, distance_type = "5f212y"
                },
                new DistanceType() {
                distance_type_id = 250, distance_type = "2m128y"
                },
                new DistanceType() {
                distance_type_id = 251, distance_type = "2m3f"
                },
                new DistanceType() {
                distance_type_id = 252, distance_type = "1m2f42y"
                },
                new DistanceType() {
                distance_type_id = 253, distance_type = "2m4f28y"
                },
                new DistanceType() {
                distance_type_id = 254, distance_type = "7f219y"
                },
                new DistanceType() {
                distance_type_id = 255, distance_type = "1m7f189y"
                },
                new DistanceType() {
                distance_type_id = 256, distance_type = "2m4f11y"
                },
                new DistanceType() {
                distance_type_id = 257, distance_type = "1m3f180y"
                },
                new DistanceType() {
                distance_type_id = 258, distance_type = "2m1f14y"
                },
                new DistanceType() {
                distance_type_id = 259, distance_type = "7f162y"
                },
                new DistanceType() {
                distance_type_id = 260, distance_type = "3m45y"
                },
                new DistanceType() {
                distance_type_id = 261, distance_type = "1m7f133y"
                },
                new DistanceType() {
                distance_type_id = 262, distance_type = "3m"
                },
                new DistanceType() {
                distance_type_id = 263, distance_type = "7f37y"
                },
                new DistanceType() {
                distance_type_id = 264, distance_type = "1m3y"
                },
                new DistanceType() {
                distance_type_id = 265, distance_type = "2m4f1y"
                },
                new DistanceType() {
                distance_type_id = 266, distance_type = "1m2f110y"
                },
                new DistanceType() {
                distance_type_id = 267, distance_type = "3m149y"
                },
                new DistanceType() {
                distance_type_id = 268, distance_type = "1m6f55y"
                },
                new DistanceType() {
                distance_type_id = 269, distance_type = "2m4f199y"
                },
                new DistanceType() {
                distance_type_id = 270, distance_type = "2m7f131y"
                },
                new DistanceType() {
                distance_type_id = 271, distance_type = "1m7f182y"
                },
                new DistanceType() {
                distance_type_id = 272, distance_type = "1m2f219y"
                },
                new DistanceType() {
                distance_type_id = 273, distance_type = "2m3f209y"
                },
                new DistanceType() {
                distance_type_id = 274, distance_type = "2m3f207y"
                },
                new DistanceType() {
                distance_type_id = 275, distance_type = "2m3f55y"
                },
                new DistanceType() {
                distance_type_id = 276, distance_type = "1m1f35y"
                },
                new DistanceType() {
                distance_type_id = 277, distance_type = "3m210y"
                },
                new DistanceType() {
                distance_type_id = 278, distance_type = "2m7f198y"
                },
                new DistanceType() {
                distance_type_id = 279, distance_type = "2m1f36y"
                },
                new DistanceType() {
                distance_type_id = 280, distance_type = "7f1y"
                },
                new DistanceType() {
                distance_type_id = 281, distance_type = "2m1f162y"
                },
                new DistanceType() {
                distance_type_id = 282, distance_type = "2m1f105y"
                },
                new DistanceType() {
                distance_type_id = 283, distance_type = "2m7f160y"
                },
                new DistanceType() {
                distance_type_id = 284, distance_type = "2m4f110y"
                },
                new DistanceType() {
                distance_type_id = 285, distance_type = "2m7f"
                },
                new DistanceType() {
                distance_type_id = 286, distance_type = "5f8y"
                },
                new DistanceType() {
                distance_type_id = 287, distance_type = "1m1f104y"
                },
                new DistanceType() {
                distance_type_id = 288, distance_type = "2m1f77y"
                },
                new DistanceType() {
                distance_type_id = 289, distance_type = "7f100y"
                },
                new DistanceType() {
                distance_type_id = 290, distance_type = "7f33y"
                },
                new DistanceType() {
                distance_type_id = 291, distance_type = "1m3f184y"
                },
                new DistanceType() {
                distance_type_id = 292, distance_type = "2m5f122y"
                },
                new DistanceType() {
                distance_type_id = 293, distance_type = "3m110y"
                },
                new DistanceType() {
                distance_type_id = 294, distance_type = "2m4f142y"
                },
                new DistanceType() {
                distance_type_id = 295, distance_type = "2m6f110y"
                },
                new DistanceType() {
                distance_type_id = 296, distance_type = "3m52y"
                },
                new DistanceType() {
                distance_type_id = 297, distance_type = "2m6f108y"
                },
                new DistanceType() {
                distance_type_id = 298, distance_type = "6f150y"
                },
                new DistanceType() {
                distance_type_id = 299, distance_type = "3m1f166y"
                },
                new DistanceType() {
                distance_type_id = 300, distance_type = "2m53y"
                },
                new DistanceType() {
                distance_type_id = 301, distance_type = "5f34y"
                },
                new DistanceType() {
                distance_type_id = 302, distance_type = "2m6f125y"
                },
                new DistanceType() {
                distance_type_id = 303, distance_type = "2m1f160y"
                },
                new DistanceType() {
                distance_type_id = 304, distance_type = "1m5f26y"
                },
                new DistanceType() {
                distance_type_id = 305, distance_type = "7f36y"
                },
                new DistanceType() {
                distance_type_id = 306, distance_type = "1m3f218y"
                },
                new DistanceType() {
                distance_type_id = 307, distance_type = "2m3f58y"
                },
                new DistanceType() {
                distance_type_id = 308, distance_type = "2m3f85y"
                },
                new DistanceType() {
                distance_type_id = 309, distance_type = "3m26y"
                },
                new DistanceType() {
                distance_type_id = 310, distance_type = "2m3f120y"
                },
                new DistanceType() {
                distance_type_id = 311, distance_type = "1m6f"
                },
                new DistanceType() {
                distance_type_id = 312, distance_type = "3m41y"
                },
                new DistanceType() {
                distance_type_id = 313, distance_type = "2m7f191y"
                },
                new DistanceType() {
                distance_type_id = 314, distance_type = "3m48y"
                },
                new DistanceType() {
                distance_type_id = 315, distance_type = "2m47y"
                },
                new DistanceType() {
                distance_type_id = 316, distance_type = "2m2f110y"
                },
                new DistanceType() {
                distance_type_id = 317, distance_type = "3m54y"
                },
                new DistanceType() {
                distance_type_id = 318, distance_type = "1m5f219y"
                },
                new DistanceType() {
                distance_type_id = 319, distance_type = "2m5f28y"
                },
                new DistanceType() {
                distance_type_id = 320, distance_type = "1m3f190y"
                },
                new DistanceType() {
                distance_type_id = 321, distance_type = "2m6f151y"
                },
                new DistanceType() {
                distance_type_id = 322, distance_type = "2m4f8y"
                },
                new DistanceType() {
                distance_type_id = 323, distance_type = "2m5f133y"
                },
                new DistanceType() {
                distance_type_id = 324, distance_type = "7f50y"
                },
                new DistanceType() {
                distance_type_id = 325, distance_type = "2m5f55y"
                },
                new DistanceType() {
                distance_type_id = 326, distance_type = "1m7f36y"
                },
                new DistanceType() {
                distance_type_id = 327, distance_type = "2m2f140y"
                },
                new DistanceType() {
                distance_type_id = 328, distance_type = "1m7f207y"
                },
                new DistanceType() {
                distance_type_id = 329, distance_type = "1m2f44y"
                },
                new DistanceType() {
                distance_type_id = 330, distance_type = "2m3f164y"
                },
                new DistanceType() {
                distance_type_id = 331, distance_type = "2m5f163y"
                },
                new DistanceType() {
                distance_type_id = 332, distance_type = "3m2f162y"
                },
                new DistanceType() {
                distance_type_id = 333, distance_type = "1m55y"
                },
                new DistanceType() {
                distance_type_id = 334, distance_type = "7f192y"
                },
                new DistanceType() {
                distance_type_id = 335, distance_type = "1m3f104y"
                },
                new DistanceType() {
                distance_type_id = 336, distance_type = "1m6f44y"
                },
                new DistanceType() {
                distance_type_id = 337, distance_type = "1m5y"
                },
                new DistanceType() {
                distance_type_id = 338, distance_type = "1m6f34y"
                },
                new DistanceType() {
                distance_type_id = 339, distance_type = "3m1f30y"
                },
                new DistanceType() {
                distance_type_id = 340, distance_type = "2m110y"
                },
                new DistanceType() {
                distance_type_id = 341, distance_type = "5f164y"
                },
                new DistanceType() {
                distance_type_id = 342, distance_type = "2m7f208y"
                },
                new DistanceType() {
                distance_type_id = 343, distance_type = "2m7f36y"
                },
                new DistanceType() {
                distance_type_id = 344, distance_type = "3m5f197y"
                },
                new DistanceType() {
                distance_type_id = 345, distance_type = "2m120y"
                },
                new DistanceType() {
                distance_type_id = 346, distance_type = "3m37y"
                },
                new DistanceType() {
                distance_type_id = 347, distance_type = "1m2f1y"
                },
                new DistanceType() {
                distance_type_id = 348, distance_type = "1m5f21y"
                },
                new DistanceType() {
                distance_type_id = 349, distance_type = "1m2f50y"
                },
                new DistanceType() {
                distance_type_id = 350, distance_type = "1m2f70y"
                },
                new DistanceType() {
                distance_type_id = 351, distance_type = "2m3f170y"
                },
                new DistanceType() {
                distance_type_id = 352, distance_type = "2m7f180y"
                },
                new DistanceType() {
                distance_type_id = 353, distance_type = "2m4f216y"
                },
                new DistanceType() {
                distance_type_id = 354, distance_type = "1m4f104y"
                },
                new DistanceType() {
                distance_type_id = 355, distance_type = "2m56y"
                },
                new DistanceType() {
                distance_type_id = 356, distance_type = "1m5f172y"
                },
                new DistanceType() {
                distance_type_id = 357, distance_type = "1m3f219y"
                },
                new DistanceType() {
                distance_type_id = 358, distance_type = "1m7f170y"
                },
                new DistanceType() {
                distance_type_id = 359, distance_type = "2m5f34y"
                },
                new DistanceType() {
                distance_type_id = 360, distance_type = "2m4f145y"
                },
                new DistanceType() {
                distance_type_id = 361, distance_type = "1m"
                },
                new DistanceType() {
                distance_type_id = 362, distance_type = "2m5f164y"
                },
                new DistanceType() {
                distance_type_id = 363, distance_type = "2m4f156y"
                },
                new DistanceType() {
                distance_type_id = 364, distance_type = "2m2f66y"
                },
                new DistanceType() {
                distance_type_id = 365, distance_type = "2m7f177y"
                },
                new DistanceType() {
                distance_type_id = 366, distance_type = "1m2f5y"
                },
                new DistanceType() {
                distance_type_id = 367, distance_type = "2m7f207y"
                },
                new DistanceType() {
                distance_type_id = 368, distance_type = "1m6y"
                },
                new DistanceType() {
                distance_type_id = 369, distance_type = "2m4f20y"
                },
                new DistanceType() {
                distance_type_id = 370, distance_type = "1m5f11y"
                },
                new DistanceType() {
                distance_type_id = 371, distance_type = "1m6f87y"
                },
                new DistanceType() {
                distance_type_id = 372, distance_type = "2m3f198y"
                },
                new DistanceType() {
                distance_type_id = 373, distance_type = "2m4f189y"
                },
                new DistanceType() {
                distance_type_id = 374, distance_type = "2m2f"
                },
                new DistanceType() {
                distance_type_id = 375, distance_type = "1m1f207y"
                },
                new DistanceType() {
                distance_type_id = 376, distance_type = "2m3f148y"
                },
                new DistanceType() {
                distance_type_id = 377, distance_type = "3m20y"
                },
                new DistanceType() {
                distance_type_id = 378, distance_type = "2m3f19y"
                },
                new DistanceType() {
                distance_type_id = 379, distance_type = "1m5f192y"
                },
                new DistanceType() {
                distance_type_id = 380, distance_type = "2m161y"
                },
                new DistanceType() {
                distance_type_id = 381, distance_type = "2m1f24y"
                },
                new DistanceType() {
                distance_type_id = 382, distance_type = "6f110y"
                },
                new DistanceType() {
                distance_type_id = 383, distance_type = "1m5f110y"
                },
                new DistanceType() {
                distance_type_id = 384, distance_type = "2m3f98y"
                },
                new DistanceType() {
                distance_type_id = 385, distance_type = "2m9y"
                },
                new DistanceType() {
                distance_type_id = 386, distance_type = "2m82y"
                },
                new DistanceType() {
                distance_type_id = 387, distance_type = "2m3f197y"
                },
                new DistanceType() {
                distance_type_id = 388, distance_type = "2m4f149y"
                },
                new DistanceType() {
                distance_type_id = 389, distance_type = "2m2f40y"
                },
                new DistanceType() {
                distance_type_id = 390, distance_type = "2m7f3y"
                },
                new DistanceType() {
                distance_type_id = 391, distance_type = "2m4f44y"
                },
                new DistanceType() {
                distance_type_id = 392, distance_type = "3m6f37y"
                },
                new DistanceType() {
                distance_type_id = 393, distance_type = "2m3f150y"
                },
                new DistanceType() {
                distance_type_id = 394, distance_type = "2m180y"
                },
                new DistanceType() {
                distance_type_id = 395, distance_type = "2m20y"
                },
                new DistanceType() {
                distance_type_id = 396, distance_type = "3m40y"
                },
                new DistanceType() {
                distance_type_id = 397, distance_type = "3m2f13y"
                },
                new DistanceType() {
                distance_type_id = 398, distance_type = "2m6f140y"
                },
                new DistanceType() {
                distance_type_id = 399, distance_type = "2m2f20y"
                },
                new DistanceType() {
                distance_type_id = 400, distance_type = "2m40y"
                },
                new DistanceType() {
                distance_type_id = 401, distance_type = "2m5f80y"
                },
                new DistanceType() {
                distance_type_id = 402, distance_type = "2m7f190y"
                },
                new DistanceType() {
                distance_type_id = 403, distance_type = "3m3f149y"
                },
                new DistanceType() {
                distance_type_id = 404, distance_type = "3m3f71y"
                },
                new DistanceType() {
                distance_type_id = 405, distance_type = "3m4f102y"
                },
                new DistanceType() {
                distance_type_id = 406, distance_type = "1m7f113y"
                },
                new DistanceType() {
                distance_type_id = 407, distance_type = "2m7f95y"
                },
                new DistanceType() {
                distance_type_id = 408, distance_type = "3m7f199y"
                },
                new DistanceType() {
                distance_type_id = 409, distance_type = "2m3f83y"
                },
                new DistanceType() {
                distance_type_id = 410, distance_type = "2m5f192y"
                },
                new DistanceType() {
                distance_type_id = 411, distance_type = "2m7f51y"
                },
                new DistanceType() {
                distance_type_id = 412, distance_type = "2m4f85y"
                },
                new DistanceType() {
                distance_type_id = 413, distance_type = "2m33y"
                },
                new DistanceType() {
                distance_type_id = 414, distance_type = "2m7f16y"
                },
                new DistanceType() {
                distance_type_id = 415, distance_type = "2m7f91y"
                },
                new DistanceType() {
                distance_type_id = 416, distance_type = "2m7f149y"
                },
                new DistanceType() {
                distance_type_id = 417, distance_type = "2m4f62y"
                },
                new DistanceType() {
                distance_type_id = 418, distance_type = "2m4f19y"
                },
                new DistanceType() {
                distance_type_id = 419, distance_type = "1m7f149y"
                },
                new DistanceType() {
                distance_type_id = 420, distance_type = "3m1f71y"
                },
                new DistanceType() {
                distance_type_id = 421, distance_type = "1m7f156y"
                },
                new DistanceType() {
                distance_type_id = 422, distance_type = "2m3f51y"
                },
                new DistanceType() {
                distance_type_id = 423, distance_type = "2m3f66y"
                },
                new DistanceType() {
                distance_type_id = 424, distance_type = "3m1f54y"
                },
                new DistanceType() {
                distance_type_id = 425, distance_type = "2m5f8y"
                },
                new DistanceType() {
                distance_type_id = 426, distance_type = "2m5f141y"
                },
                new DistanceType() {
                distance_type_id = 427, distance_type = "1m7f144y"
                },
                new DistanceType() {
                distance_type_id = 428, distance_type = "3m4f97y"
                },
                new DistanceType() {
                distance_type_id = 429, distance_type = "2m2f191y"
                },
                new DistanceType() {
                distance_type_id = 430, distance_type = "2m5f127y"
                },
                new DistanceType() {
                distance_type_id = 431, distance_type = "3m58y"
                },
                new DistanceType() {
                distance_type_id = 432, distance_type = "2m7f118y"
                },
                new DistanceType() {
                distance_type_id = 433, distance_type = "2m4f88y"
                },
                new DistanceType() {
                distance_type_id = 434, distance_type = "3m60y"
                },
                new DistanceType() {
                distance_type_id = 435, distance_type = "1m7f188y"
                },
                new DistanceType() {
                distance_type_id = 436, distance_type = "1m5f66y"
                },
                new DistanceType() {
                distance_type_id = 437, distance_type = "2m4f118y"
                },
                new DistanceType() {
                distance_type_id = 438, distance_type = "2m92y"
                },
                new DistanceType() {
                distance_type_id = 439, distance_type = "2m3f31y"
                },
                new DistanceType() {
                distance_type_id = 440, distance_type = "2m4f181y"
                },
                new DistanceType() {
                distance_type_id = 441, distance_type = "2m7f214y"
                },
                new DistanceType() {
                distance_type_id = 442, distance_type = "2m3f88y"
                },
                new DistanceType() {
                distance_type_id = 443, distance_type = "3m84y"
                },
                new DistanceType() {
                distance_type_id = 444, distance_type = "2m78y"
                },
                new DistanceType() {
                distance_type_id = 445, distance_type = "3m2f1y"
                },
                new DistanceType() {
                distance_type_id = 446, distance_type = "2m7f86y"
                },
                new DistanceType() {
                distance_type_id = 447, distance_type = "3m1f214y"
                },
                new DistanceType() {
                distance_type_id = 448, distance_type = "2m3f175y"
                },
                new DistanceType() {
                distance_type_id = 449, distance_type = "3m5f"
                },
                new DistanceType() {
                distance_type_id = 450, distance_type = "2m3f61y"
                },
                new DistanceType() {
                distance_type_id = 451, distance_type = "2m3f14y"
                },
                new DistanceType() {
                distance_type_id = 452, distance_type = "2m43y"
                },
                new DistanceType() {
                distance_type_id = 453, distance_type = "2m1f191y"
                },
                new DistanceType() {
                distance_type_id = 454, distance_type = "3m150y"
                },
                new DistanceType() {
                distance_type_id = 455, distance_type = "2m5f137y"
                },
                new DistanceType() {
                distance_type_id = 456, distance_type = "3m6f153y"
                },
                new DistanceType() {
                distance_type_id = 457, distance_type = "1m7f119y"
                },
                new DistanceType() {
                distance_type_id = 458, distance_type = "2m7f98y"
                },
                new DistanceType() {
                distance_type_id = 459, distance_type = "3m1f188y"
                },
                new DistanceType() {
                distance_type_id = 460, distance_type = "1m4f77y"
                },
                new DistanceType() {
                distance_type_id = 461, distance_type = "3m4f166y"
                },
                new DistanceType() {
                distance_type_id = 462, distance_type = "1m7f190y"
                },
                new DistanceType() {
                distance_type_id = 463, distance_type = "2m3f80y"
                },
                new DistanceType() {
                distance_type_id = 464, distance_type = "4m90y"
                },
                new DistanceType() {
                distance_type_id = 465, distance_type = "2m3f171y"
                },
                new DistanceType() {
                distance_type_id = 466, distance_type = "2m7f170y"
                },
                new DistanceType() {
                distance_type_id = 467, distance_type = "1m4f14y"
                },
                new DistanceType() {
                distance_type_id = 468, distance_type = "4f214y"
                },
                new DistanceType() {
                distance_type_id = 469, distance_type = "1m6f21y"
                },
                new DistanceType() {
                distance_type_id = 470, distance_type = "1m3f23y"
                },
                new DistanceType() {
                distance_type_id = 471, distance_type = "1m13y"
                },
                new DistanceType() {
                distance_type_id = 472, distance_type = "6f16y"
                },
                new DistanceType() {
                distance_type_id = 473, distance_type = "2m50y"
                },
                new DistanceType() {
                distance_type_id = 474, distance_type = "2m4f115y"
                },
                new DistanceType() {
                distance_type_id = 475, distance_type = "2m179y"
                },
                new DistanceType() {
                distance_type_id = 476, distance_type = "3m1f56y"
                },
                new DistanceType() {
                distance_type_id = 477, distance_type = "2m4f127y"
                },
                new DistanceType() {
                distance_type_id = 478, distance_type = "3m2f"
                },
                new DistanceType() {
                distance_type_id = 479, distance_type = "2m7f213y"
                },
                new DistanceType() {
                distance_type_id = 480, distance_type = "2m62y"
                },
                new DistanceType() {
                distance_type_id = 481, distance_type = "2m4f56y"
                },
                new DistanceType() {
                distance_type_id = 482, distance_type = "2m4f198y"
                },
                new DistanceType() {
                distance_type_id = 483, distance_type = "3m1f185y"
                },
                new DistanceType() {
                distance_type_id = 484, distance_type = "1m7f145y"
                },
                new DistanceType() {
                distance_type_id = 485, distance_type = "1m7f201y"
                },
                new DistanceType() {
                distance_type_id = 486, distance_type = "2m4f25y"
                },
                new DistanceType() {
                distance_type_id = 487, distance_type = "1m4f143y"
                },
                new DistanceType() {
                distance_type_id = 488, distance_type = "2m2f187y"
                },
                new DistanceType() {
                distance_type_id = 489, distance_type = "1m7f180y"
                },
                new DistanceType() {
                distance_type_id = 490, distance_type = "3m1f60y"
                },
                new DistanceType() {
                distance_type_id = 491, distance_type = "2m6f45y"
                },
                new DistanceType() {
                distance_type_id = 492, distance_type = "2m2f98y"
                },
                new DistanceType() {
                distance_type_id = 493, distance_type = "2m102y"
                },
                new DistanceType() {
                distance_type_id = 494, distance_type = "2m67y"
                },
                new DistanceType() {
                distance_type_id = 495, distance_type = "1m6f66y"
                },
                new DistanceType() {
                distance_type_id = 496, distance_type = "3m97y"
                },
                new DistanceType() {
                distance_type_id = 497, distance_type = "3m182y"
                },
                new DistanceType() {
                distance_type_id = 498, distance_type = "2m6y"
                },
                new DistanceType() {
                distance_type_id = 499, distance_type = "2m7f69y"
                },
                new DistanceType() {
                distance_type_id = 500, distance_type = "3m5f24y"
                },
                new DistanceType() {
                distance_type_id = 501, distance_type = "2m5f91y"
                },
                new DistanceType() {
                distance_type_id = 502, distance_type = "2m3f160y"
                },
                new DistanceType() {
                distance_type_id = 503, distance_type = "3m3f123y"
                },
                new DistanceType() {
                distance_type_id = 504, distance_type = "2m4f50y"
                },
                new DistanceType() {
                distance_type_id = 505, distance_type = "2m1f50y"
                },
                new DistanceType() {
                distance_type_id = 506, distance_type = "2m6f120y"
                },
                new DistanceType() {
                distance_type_id = 507, distance_type = "3m6f130y"
                },
                new DistanceType() {
                distance_type_id = 508, distance_type = "3m121y"
                },
                new DistanceType() {
                distance_type_id = 509, distance_type = "2m6f70y"
                },
                new DistanceType() {
                distance_type_id = 510, distance_type = "3m2f57y"
                },
                new DistanceType() {
                distance_type_id = 511, distance_type = "2m6f54y"
                },
                new DistanceType() {
                distance_type_id = 512, distance_type = "2m7f40y"
                },
                new DistanceType() {
                distance_type_id = 513, distance_type = "1m7f120y"
                },
                new DistanceType() {
                distance_type_id = 514, distance_type = "2m3f30y"
                },
                new DistanceType() {
                distance_type_id = 515, distance_type = "2m2f150y"
                },
                new DistanceType() {
                distance_type_id = 516, distance_type = "1m7f90y"
                },
                new DistanceType() {
                distance_type_id = 517, distance_type = "3m2f70y"
                },
                new DistanceType() {
                distance_type_id = 518, distance_type = "1m5f209y"
                },
                new DistanceType() {
                distance_type_id = 519, distance_type = "6f165y"
                },
                new DistanceType() {
                distance_type_id = 520, distance_type = "2m164y"
                },
                new DistanceType() {
                distance_type_id = 521, distance_type = "2m3f182y"
                },
                new DistanceType() {
                distance_type_id = 522, distance_type = "2m106y"
                },
                new DistanceType() {
                distance_type_id = 523, distance_type = "2m5f75y"
                },
                new DistanceType() {
                distance_type_id = 524, distance_type = "2m7f165y"
                },
                new DistanceType() {
                distance_type_id = 525, distance_type = "2m12y"
                },
                new DistanceType() {
                distance_type_id = 526, distance_type = "3m5f214y"
                },
                new DistanceType() {
                distance_type_id = 527, distance_type = "3m2f59y"
                },
                new DistanceType() {
                distance_type_id = 528, distance_type = "1m4f11y"
                },
                new DistanceType() {
                distance_type_id = 529, distance_type = "3m5f54y"
                },
                new DistanceType() {
                distance_type_id = 530, distance_type = "2m4f200y"
                },
                new DistanceType() {
                distance_type_id = 531, distance_type = "2m4f37y"
                },
                new DistanceType() {
                distance_type_id = 532, distance_type = "2m6f40y"
                },
                new DistanceType() {
                distance_type_id = 533, distance_type = "2m4f130y"
                },
                new DistanceType() {
                distance_type_id = 534, distance_type = "1m6f7y"
                },
                new DistanceType() {
                distance_type_id = 535, distance_type = "2m3f203y"
                },
                new DistanceType() {
                distance_type_id = 536, distance_type = "3m4f85y"
                },
                new DistanceType() {
                distance_type_id = 537, distance_type = "1m7f101y"
                },
                new DistanceType() {
                distance_type_id = 538, distance_type = "2m4f63y"
                },
                new DistanceType() {
                distance_type_id = 539, distance_type = "2m5f144y"
                },
                new DistanceType() {
                distance_type_id = 540, distance_type = "3m136y"
                },
                new DistanceType() {
                distance_type_id = 541, distance_type = "2m6f10y"
                },
                new DistanceType() {
                distance_type_id = 542, distance_type = "3m4f178y"
                },
                new DistanceType() {
                distance_type_id = 543, distance_type = "2m150y"
                },
                new DistanceType() {
                distance_type_id = 544, distance_type = "3m100y"
                },
                new DistanceType() {
                distance_type_id = 545, distance_type = "1m6f11y"
                },
                new DistanceType() {
                distance_type_id = 546, distance_type = "3m1f110y"
                },
                new DistanceType() {
                distance_type_id = 547, distance_type = "2m77y"
                },
                new DistanceType() {
                distance_type_id = 548, distance_type = "3m170y"
                },
                new DistanceType() {
                distance_type_id = 549, distance_type = "2m4f90y"
                },
                new DistanceType() {
                distance_type_id = 550, distance_type = "2m4f70y"
                },
                new DistanceType() {
                distance_type_id = 551, distance_type = "7f15y"
                },
                new DistanceType() {
                distance_type_id = 552, distance_type = "3m7f108y"
                },
                new DistanceType() {
                distance_type_id = 553, distance_type = "3m2f139y"
                },
                new DistanceType() {
                distance_type_id = 554, distance_type = "3m61y"
                },
                new DistanceType() {
                distance_type_id = 555, distance_type = "2m1f156y"
                },
                new DistanceType() {
                distance_type_id = 556, distance_type = "2m5f61y"
                },
                new DistanceType() {
                distance_type_id = 557, distance_type = "2m3f165y"
                },
                new DistanceType() {
                distance_type_id = 558, distance_type = "3m3f9y"
                },
                new DistanceType() {
                distance_type_id = 559, distance_type = "1m7f159y"
                },
                new DistanceType() {
                distance_type_id = 560, distance_type = "2m4f29y"
                },
                new DistanceType() {
                distance_type_id = 561, distance_type = "3m5f142y"
                },
                new DistanceType() {
                distance_type_id = 562, distance_type = "1m7f131y"
                },
                new DistanceType() {
                distance_type_id = 563, distance_type = "3m4f40y"
                },
                new DistanceType() {
                distance_type_id = 564, distance_type = "2m7f115y"
                },
                new DistanceType() {
                distance_type_id = 565, distance_type = "2m2f193y"
                },
                new DistanceType() {
                distance_type_id = 566, distance_type = "1m14y"
                },
                new DistanceType() {
                distance_type_id = 567, distance_type = "1m4f1y"
                },
                new DistanceType() {
                distance_type_id = 568, distance_type = "7f2y"
                },
                new DistanceType() {
                distance_type_id = 569, distance_type = "2m4f40y"
                },
                new DistanceType() {
                distance_type_id = 570, distance_type = "2m6f31y"
                },
                new DistanceType() {
                distance_type_id = 571, distance_type = "3m219y"
                },
                new DistanceType() {
                distance_type_id = 572, distance_type = "2m4f146y"
                },
                new DistanceType() {
                distance_type_id = 573, distance_type = "2m7f138y"
                },
                new DistanceType() {
                distance_type_id = 574, distance_type = "2m66y"
                },
                new DistanceType() {
                distance_type_id = 575, distance_type = "4m1f56y"
                },
                new DistanceType() {
                distance_type_id = 576, distance_type = "6f166y"
                },
                new DistanceType() {
                distance_type_id = 577, distance_type = "2m2f208y"
                },
                new DistanceType() {
                distance_type_id = 578, distance_type = "1m1f219y"
                },
                new DistanceType() {
                distance_type_id = 579, distance_type = "2m2f152y"
                },
                new DistanceType() {
                distance_type_id = 580, distance_type = "2m2f61y"
                },
                new DistanceType() {
                distance_type_id = 581, distance_type = "2m4f166y"
                },
                new DistanceType() {
                distance_type_id = 582, distance_type = "2m98y"
                },
                new DistanceType() {
                distance_type_id = 583, distance_type = "2m4f218y"
                },
                new DistanceType() {
                distance_type_id = 584, distance_type = "2m7f186y"
                },
                new DistanceType() {
                distance_type_id = 585, distance_type = "2m6f90y"
                },
                new DistanceType() {
                distance_type_id = 586, distance_type = "3m120y"
                },
                new DistanceType() {
                distance_type_id = 587, distance_type = "1m7f80y"
                },
                new DistanceType() {
                distance_type_id = 588, distance_type = "3m126y"
                },
                new DistanceType() {
                distance_type_id = 589, distance_type = "2m213y"
                },
                new DistanceType() {
                distance_type_id = 590, distance_type = "5f55y"
                },
                new DistanceType() {
                distance_type_id = 591, distance_type = "3m5f201y"
                },
                new DistanceType() {
                distance_type_id = 592, distance_type = "2m3f168y"
                },
                new DistanceType() {
                distance_type_id = 593, distance_type = "2m3f140y"
                },
                new DistanceType() {
                distance_type_id = 594, distance_type = "3m2f50y"
                },
                new DistanceType() {
                distance_type_id = 595, distance_type = "4m2f8y"
                },
                new DistanceType() {
                distance_type_id = 596, distance_type = "2m4f102y"
                },
                new DistanceType() {
                distance_type_id = 597, distance_type = "1m7f116y"
                },
                new DistanceType() {
                distance_type_id = 598, distance_type = "3m4f120y"
                },
                new DistanceType() {
                distance_type_id = 599, distance_type = "2m7f100y"
                },
                new DistanceType() {
                distance_type_id = 600, distance_type = "2m1f65y"
                },
                new DistanceType() {
                distance_type_id = 601, distance_type = "2m2f112y"
                },
                new DistanceType() {
                distance_type_id = 602, distance_type = "2m36y"
                },
                new DistanceType() {
                distance_type_id = 603, distance_type = "2m3f202y"
                },
                new DistanceType() {
                distance_type_id = 604, distance_type = "2m1f87y"
                },
                new DistanceType() {
                distance_type_id = 605, distance_type = "1m1f55y"
                },
                new DistanceType() {
                distance_type_id = 606, distance_type = "1m5f159y"
                },
                new DistanceType() {
                distance_type_id = 607, distance_type = "2m52y"
                },
                new DistanceType() {
                distance_type_id = 608, distance_type = "2m2f183y"
                },
                new DistanceType() {
                distance_type_id = 609, distance_type = "3m7f176y"
                },
                new DistanceType() {
                distance_type_id = 610, distance_type = "1m2f40y"
                },
                new DistanceType() {
                distance_type_id = 611, distance_type = "6f12y"
                },
                new DistanceType() {
                distance_type_id = 612, distance_type = "1m31y"
                },
                new DistanceType() {
                distance_type_id = 613, distance_type = "3m3f208y"
                },
                new DistanceType() {
                distance_type_id = 614, distance_type = "1m1f130y"
                },
                new DistanceType() {
                distance_type_id = 615, distance_type = "1m5f160y"
                },
                new DistanceType() {
                distance_type_id = 616, distance_type = "2m103y"
                },
                new DistanceType() {
                distance_type_id = 617, distance_type = "2m1f142y"
                },
                new DistanceType() {
                distance_type_id = 618, distance_type = "2m6f22y"
                },
                new DistanceType() {
                distance_type_id = 619, distance_type = "1m7f125y"
                },
                new DistanceType() {
                distance_type_id = 620, distance_type = "4m2f74y"
                },
                new DistanceType() {
                distance_type_id = 621, distance_type = "1m4f8y"
                },
                new DistanceType() {
                distance_type_id = 622, distance_type = "7f218y"
                },
                new DistanceType() {
                distance_type_id = 623, distance_type = "3m2f83y"
                },
                new DistanceType() {
                distance_type_id = 624, distance_type = "3m3f119y"
                },
                new DistanceType() {
                distance_type_id = 625, distance_type = "2m5f126y"
                },
                new DistanceType() {
                distance_type_id = 626, distance_type = "1m7f22y"
                },
                new DistanceType() {
                distance_type_id = 627, distance_type = "1m3f99y"
                },
                new DistanceType() {
                distance_type_id = 628, distance_type = "2m5f139y"
                },
                new DistanceType() {
                distance_type_id = 629, distance_type = "2m4f120y"
                },
                new DistanceType() {
                distance_type_id = 630, distance_type = "2m5f175y"
                },
                new DistanceType() {
                distance_type_id = 631, distance_type = "3m2f197y"
                },
                new DistanceType() {
                distance_type_id = 632, distance_type = "1m4f23y"
                },
                new DistanceType() {
                distance_type_id = 633, distance_type = "1m1f170y"
                },
                new DistanceType() {
                distance_type_id = 634, distance_type = "2m4f64y"
                },
                new DistanceType() {
                distance_type_id = 635, distance_type = "2m6f169y"
                },
                new DistanceType() {
                distance_type_id = 636, distance_type = "2m121y"
                },
                new DistanceType() {
                distance_type_id = 637, distance_type = "1m2y"
                },
                new DistanceType() {
                distance_type_id = 638, distance_type = "1m5f216y"
                },
                new DistanceType() {
                distance_type_id = 639, distance_type = "1m4f25y"
                },
                new DistanceType() {
                distance_type_id = 640, distance_type = "1m2f30y"
                },
                new DistanceType() {
                distance_type_id = 641, distance_type = "3m1f88y"
                },
                new DistanceType() {
                distance_type_id = 642, distance_type = "2m5f88y"
                },
                new DistanceType() {
                distance_type_id = 643, distance_type = "2m5f50y"
                },
                new DistanceType() {
                distance_type_id = 644, distance_type = "3m5f50y"
                },
                new DistanceType() {
                distance_type_id = 645, distance_type = "1m7f217y"
                },
                new DistanceType() {
                distance_type_id = 646, distance_type = "7f180y"
                },
                new DistanceType() {
                distance_type_id = 647, distance_type = "1m6f70y"
                },
                new DistanceType() {
                distance_type_id = 648, distance_type = "1m4f27y"
                },
                new DistanceType() {
                distance_type_id = 649, distance_type = "3m2f127y"
                },
                new DistanceType() {
                distance_type_id = 650, distance_type = "1m4f100y"
                },
                new DistanceType() {
                distance_type_id = 651, distance_type = "1m7f30y"
                },
                new DistanceType() {
                distance_type_id = 652, distance_type = "2m109y"
                },
                new DistanceType() {
                distance_type_id = 653, distance_type = "3m6f121y"
                },
                new DistanceType() {
                distance_type_id = 654, distance_type = "1m3f140y"
                },
                new DistanceType() {
                distance_type_id = 655, distance_type = "6f212y"
                },
                new DistanceType() {
                distance_type_id = 656, distance_type = "7f212y"
                },
                new DistanceType() {
                distance_type_id = 657, distance_type = "1m3f55y"
                },
                new DistanceType() {
                distance_type_id = 658, distance_type = "2m6f164y"
                },
                new DistanceType() {
                distance_type_id = 659, distance_type = "7f213y"
                },
                new DistanceType() {
                distance_type_id = 660, distance_type = "2m3f180y"
                },
                new DistanceType() {
                distance_type_id = 661, distance_type = "4m2f"
                },
                new DistanceType() {
                distance_type_id = 662, distance_type = "2m7f130y"
                },
                new DistanceType() {
                distance_type_id = 663, distance_type = "5f16y"
                },
                new DistanceType() {
                distance_type_id = 664, distance_type = "4m97y"
                },
                new DistanceType() {
                distance_type_id = 665, distance_type = "3m7f"
                },
                new DistanceType() {
                distance_type_id = 666, distance_type = "1m5f16y"
                },
                new DistanceType() {
                distance_type_id = 667, distance_type = "1m3f15y"
                },
                new DistanceType() {
                distance_type_id = 668, distance_type = "5f195y"
                },
                new DistanceType() {
                distance_type_id = 669, distance_type = "1m5f70y"
                },
                new DistanceType() {
                distance_type_id = 670, distance_type = "1m2f142y"
                },
                new DistanceType() {
                distance_type_id = 671, distance_type = "2m7f50y"
                },
                new DistanceType() {
                distance_type_id = 672, distance_type = "2m1f26y"
                },
                new DistanceType() {
                distance_type_id = 673, distance_type = "1m3f75y"
                },
                new DistanceType() {
                distance_type_id = 674, distance_type = "1m4f63y"
                },
                new DistanceType() {
                distance_type_id = 675, distance_type = "3m3f189y"
                },
                new DistanceType() {
                distance_type_id = 676, distance_type = "1m5f170y"
                },
                new DistanceType() {
                distance_type_id = 677, distance_type = "3m6f162y"
                },
                new DistanceType() {
                distance_type_id = 678, distance_type = "2m1f60y"
                },
                new DistanceType() {
                distance_type_id = 679, distance_type = "2m5f180y"
                },
                new DistanceType() {
                distance_type_id = 680, distance_type = "2m1f100y"
                },
                new DistanceType() {
                distance_type_id = 681, distance_type = "1m3f175y"
                },
                new DistanceType() {
                distance_type_id = 682, distance_type = "1m3f133y"
                },
                new DistanceType() {
                distance_type_id = 683, distance_type = "7f70y"
                },
                new DistanceType() {
                distance_type_id = 684, distance_type = "1m3f213y"
                },
                new DistanceType() {
                distance_type_id = 685, distance_type = "2m95y"
                },
                new DistanceType() {
                distance_type_id = 686, distance_type = "3m1f50y"
                },
                new DistanceType() {
                distance_type_id = 687, distance_type = "5f162y"
                },
                new DistanceType() {
                distance_type_id = 688, distance_type = "1m2f157y"
                },
                new DistanceType() {
                distance_type_id = 689, distance_type = "5f182y"
                },
                new DistanceType() {
                distance_type_id = 690, distance_type = "7f173y"
                },
                new DistanceType() {
                distance_type_id = 691, distance_type = "6f195y"
                },
                new DistanceType() {
                distance_type_id = 692, distance_type = "1m3f39y"
                },
                new DistanceType() {
                distance_type_id = 693, distance_type = "2m6f50y"
                },
                new DistanceType() {
                distance_type_id = 694, distance_type = "5f172y"
                },
                new DistanceType() {
                distance_type_id = 695, distance_type = "1m2f84y"
                },
                new DistanceType() {
                distance_type_id = 696, distance_type = "2m182y"
                },
                new DistanceType() {
                distance_type_id = 697, distance_type = "1m2f20y"
                },
                new DistanceType() {
                distance_type_id = 698, distance_type = "7f135y"
                },
                new DistanceType() {
                distance_type_id = 699, distance_type = "4f217y"
                },
                new DistanceType() {
                distance_type_id = 700, distance_type = "1m3f44y"
                },
                new DistanceType() {
                distance_type_id = 701, distance_type = "2m45y"
                },
                new DistanceType() {
                distance_type_id = 702, distance_type = "3m6f"
                },
                new DistanceType() {
                distance_type_id = 703, distance_type = "7f48y"
                },
                new DistanceType() {
                distance_type_id = 704, distance_type = "1m2f13y"
                },
                new DistanceType() {
                distance_type_id = 705, distance_type = "2m115y"
                },
                new DistanceType() {
                distance_type_id = 706, distance_type = "1m5f155y"
                },
                new DistanceType() {
                distance_type_id = 707, distance_type = "1m6f32y"
                },
                new DistanceType() {
                distance_type_id = 708, distance_type = "2m1f47y"
                },
                new DistanceType() {
                distance_type_id = 709, distance_type = "7f16y"
                },
                new DistanceType() {
                distance_type_id = 710, distance_type = "2m6f160y"
                },
                new DistanceType() {
                distance_type_id = 711, distance_type = "2m1f46y"
                },
                new DistanceType() {
                distance_type_id = 712, distance_type = "3m1f83y"
                },
                new DistanceType() {
                distance_type_id = 713, distance_type = "2m1f61y"
                },
                new DistanceType() {
                distance_type_id = 714, distance_type = "3m1f107y"
                },
                new DistanceType() {
                distance_type_id = 715, distance_type = "2m3f130y"
                },
                new DistanceType() {
                distance_type_id = 716, distance_type = "3m5f80y"
                },
                new DistanceType() {
                distance_type_id = 717, distance_type = "2m2f109y"
                },
                new DistanceType() {
                distance_type_id = 718, distance_type = "2m6f58y"
                },
                new DistanceType() {
                distance_type_id = 719, distance_type = "2m2f50y"
                },
                new DistanceType() {
                distance_type_id = 720, distance_type = "2m6f12y"
                },
                new DistanceType() {
                distance_type_id = 721, distance_type = "2m64y"
                },
                new DistanceType() {
                distance_type_id = 722, distance_type = "1m2f190y"
                },
                new DistanceType() {
                distance_type_id = 723, distance_type = "6f63y"
                },
                new DistanceType() {
                distance_type_id = 724, distance_type = "1m4f180y"
                },
                new DistanceType() {
                distance_type_id = 725, distance_type = "2m5f30y"
                },
                new DistanceType() {
                distance_type_id = 726, distance_type = "2m175y"
                },
                new DistanceType() {
                distance_type_id = 727, distance_type = "2m5f120y"
                },
                new DistanceType() {
                distance_type_id = 728, distance_type = "1m7f100y"
                },
                new DistanceType() {
                distance_type_id = 729, distance_type = "6f60y"
                },
                new DistanceType() {
                distance_type_id = 730, distance_type = "6f111y"
                },
                new DistanceType() {
                distance_type_id = 731, distance_type = "1m4f210y"
                },
                new DistanceType() {
                distance_type_id = 732, distance_type = "2m2f59y"
                },
                new DistanceType() {
                distance_type_id = 733, distance_type = "3m95y"
                },
                new DistanceType() {
                distance_type_id = 734, distance_type = "3m1f70y"
                },
                new DistanceType() {
                distance_type_id = 735, distance_type = "1m1f21y"
                },
                new DistanceType() {
                distance_type_id = 736, distance_type = "7f12y"
                },
                new DistanceType() {
                distance_type_id = 737, distance_type = "1m5f150y"
                },
                new DistanceType() {
                distance_type_id = 738, distance_type = "1m7f196y"
                },
                new DistanceType() {
                distance_type_id = 739, distance_type = "2m5f60y"
                },
                new DistanceType() {
                distance_type_id = 740, distance_type = "1m177y"
                },
                new DistanceType() {
                distance_type_id = 741, distance_type = "1m3f110y"
                },
                new DistanceType() {
                distance_type_id = 742, distance_type = "2m3f210y"
                },
                new DistanceType() {
                distance_type_id = 743, distance_type = "3m67y"
                },
                new DistanceType() {
                distance_type_id = 744, distance_type = "2m4f125y"
                },
                new DistanceType() {
                distance_type_id = 745, distance_type = "1m1f70y"
                },
                new DistanceType() {
                distance_type_id = 746, distance_type = "1m3f70y"
                },
                new DistanceType() {
                distance_type_id = 747, distance_type = "2m5f143y"
                },
                new DistanceType() {
                distance_type_id = 748, distance_type = "1m2f75y"
                },
                new DistanceType() {
                distance_type_id = 749, distance_type = "1m4f175y"
                },
                new DistanceType() {
                distance_type_id = 750, distance_type = "1m1f125y"
                },
                new DistanceType() {
                distance_type_id = 751, distance_type = "1m4f169y"
                },
                new DistanceType() {
                distance_type_id = 752, distance_type = "1m4f140y"
                },
                new DistanceType() {
                distance_type_id = 753, distance_type = "5f170y"
                },
                new DistanceType() {
                distance_type_id = 754, distance_type = "1m2f38y"
                },
                new DistanceType() {
                distance_type_id = 755, distance_type = "7f60y"
                },
                new DistanceType() {
                distance_type_id = 756, distance_type = "1m4f120y"
                },
                new DistanceType() {
                distance_type_id = 757, distance_type = "7f198y"
                },
                new DistanceType() {
                distance_type_id = 758, distance_type = "1m6f89y"
                },
                new DistanceType() {
                distance_type_id = 759, distance_type = "1m4f24y"
                },
                new DistanceType() {
                distance_type_id = 760, distance_type = "7f175y"
                },
                new DistanceType() {
                distance_type_id = 761, distance_type = "1m3f208y"
                },
                new DistanceType() {
                distance_type_id = 762, distance_type = "2m188y"
                },
                new DistanceType() {
                distance_type_id = 763, distance_type = "2m6f20y"
                },
                new DistanceType() {
                distance_type_id = 764, distance_type = "2m7f179y"
                },
                new DistanceType() {
                distance_type_id = 765, distance_type = "1m3f209y"
                },
                new DistanceType() {
                distance_type_id = 766, distance_type = "1m5f140y"
                },
                new DistanceType() {
                distance_type_id = 767, distance_type = "5f143y"
                },
                new DistanceType() {
                distance_type_id = 768, distance_type = "1m2f55y"
                },
                new DistanceType() {
                distance_type_id = 769, distance_type = "2m194y"
                },
                new DistanceType() {
                distance_type_id = 770, distance_type = "2m3f194y"
                },
                new DistanceType() {
                distance_type_id = 771, distance_type = "3m1f217y"
                },
                new DistanceType() {
                distance_type_id = 772, distance_type = "1m20y"
                },
                new DistanceType() {
                distance_type_id = 773, distance_type = "1m3f35y"
                },
                new DistanceType() {
                distance_type_id = 774, distance_type = "2m32y"
                },
                new DistanceType() {
                distance_type_id = 775, distance_type = "2m185y"
                },
                new DistanceType() {
                distance_type_id = 776, distance_type = "1m6f20y"
                },
                new DistanceType() {
                distance_type_id = 777, distance_type = "1m40y"
                },
                new DistanceType() {
                distance_type_id = 778, distance_type = "1m6f30y"
                },
                new DistanceType() {
                distance_type_id = 779, distance_type = "2m170y"
                },
                new DistanceType() {
                distance_type_id = 780, distance_type = "2m7f14y"
                },
                new DistanceType() {
                distance_type_id = 781, distance_type = "2m3f114y"
                },
                new DistanceType() {
                distance_type_id = 782, distance_type = "1m1f96y"
                },
                new DistanceType() {
                distance_type_id = 783, distance_type = "1m4f126y"
                },
                new DistanceType() {
                distance_type_id = 784, distance_type = "2m1f95y"
                },
                new DistanceType() {
                distance_type_id = 785, distance_type = "1m7f92y"
                },
                new DistanceType() {
                distance_type_id = 786, distance_type = "2m2f130y"
                },
                new DistanceType() {
                distance_type_id = 787, distance_type = "2m13y"
                },
                new DistanceType() {
                distance_type_id = 788, distance_type = "1m5f205y"
                },
                new DistanceType() {
                distance_type_id = 789, distance_type = "2m130y"
                },
                new DistanceType() {
                distance_type_id = 790, distance_type = "1m4f84y"
                },
                new DistanceType() {
                distance_type_id = 791, distance_type = "1m123y"
                },
                new DistanceType() {
                distance_type_id = 792, distance_type = "2m5f11y"
                },
                new DistanceType() {
                distance_type_id = 793, distance_type = "1m4f43y"
                },
                new DistanceType() {
                distance_type_id = 794, distance_type = "1m98y"
                },
                new DistanceType() {
                distance_type_id = 795, distance_type = "2m4f97y"
                },
                new DistanceType() {
                distance_type_id = 796, distance_type = "1m6f14y"
                },
                new DistanceType() {
                distance_type_id = 797, distance_type = "2m6f168y"
                },
                new DistanceType() {
                distance_type_id = 798, distance_type = "2m2f180y"
                },
                new DistanceType() {
                distance_type_id = 799, distance_type = "2m30y"
                },
                new DistanceType() {
                distance_type_id = 800, distance_type = "2m1f195y"
                },
                new DistanceType() {
                distance_type_id = 801, distance_type = "1m4f115y"
                },
                new DistanceType() {
                distance_type_id = 802, distance_type = "2m5f165y"
                },
                new DistanceType() {
                distance_type_id = 803, distance_type = "2m143y"
                },
                new DistanceType() {
                distance_type_id = 804, distance_type = "3m1f157y"
                },
                new DistanceType() {
                distance_type_id = 805, distance_type = "7f21y"
                },
                new DistanceType() {
                distance_type_id = 806, distance_type = "1m7f45y"
                },
                new DistanceType() {
                distance_type_id = 807, distance_type = "1m7f150y"
                },
                new DistanceType() {
                distance_type_id = 808, distance_type = "1m2f47y"
                },
                new DistanceType() {
                distance_type_id = 809, distance_type = "2m4f141y"
                },
                new DistanceType() {
                distance_type_id = 810, distance_type = "1m6f40y"
                },
                new DistanceType() {
                distance_type_id = 811, distance_type = "2m6f80y"
                },
                new DistanceType() {
                distance_type_id = 812, distance_type = "2m6f14y"
                },
                new DistanceType() {
                distance_type_id = 813, distance_type = "2m2f28y"
                },
                new DistanceType() {
                distance_type_id = 814, distance_type = "7f187y"
                },
                new DistanceType() {
                distance_type_id = 815, distance_type = "2m1f22y"
                },
                new DistanceType() {
                distance_type_id = 816, distance_type = "2m5f160y"
                },
                new DistanceType() {
                distance_type_id = 817, distance_type = "7f54y"
                },
                new DistanceType() {
                distance_type_id = 818, distance_type = "1m2f14y"
                },
                new DistanceType() {
                distance_type_id = 819, distance_type = "1m3f182y"
                },
                new DistanceType() {
                distance_type_id = 820, distance_type = "1m1f136y"
                },
                new DistanceType() {
                distance_type_id = 821, distance_type = "2m1f175y"
                },
                new DistanceType() {
                distance_type_id = 822, distance_type = "2m5f87y"
                },
                new DistanceType() {
                distance_type_id = 823, distance_type = "7f24y"
                },
                new DistanceType() {
                distance_type_id = 824, distance_type = "1m4f42y"
                },
                new DistanceType() {
                distance_type_id = 825, distance_type = "2m1f153y"
                },
                new DistanceType() {
                distance_type_id = 826, distance_type = "2m5f181y"
                },
                new DistanceType() {
                distance_type_id = 827, distance_type = "2m1f197y"
                },
                new DistanceType() {
                distance_type_id = 828, distance_type = "2m1f55y"
                },
                new DistanceType() {
                distance_type_id = 829, distance_type = "2m5f47y"
                },
                new DistanceType() {
                distance_type_id = 830, distance_type = "1m70y"
                },
                new DistanceType() {
                distance_type_id = 831, distance_type = "2m124y"
                },
                new DistanceType() {
                distance_type_id = 832, distance_type = "1m7f78y"
                },
                new DistanceType() {
                distance_type_id = 833, distance_type = "1m4f121y"
                },
                new DistanceType() {
                distance_type_id = 834, distance_type = "1m4f101y"
                },
                new DistanceType() {
                distance_type_id = 835, distance_type = "2m141y"
                },
                new DistanceType() {
                distance_type_id = 836, distance_type = "2m7f192y"
                },
                new DistanceType() {
                distance_type_id = 837, distance_type = "1m60y"
                },
                new DistanceType() {
                distance_type_id = 838, distance_type = "1m3f80y"
                },
                new DistanceType() {
                distance_type_id = 839, distance_type = "2m1f140y"
                },
                new DistanceType() {
                distance_type_id = 840, distance_type = "2m4f151y"
                },
                new DistanceType() {
                distance_type_id = 841, distance_type = "3m1f90y"
                },
                new DistanceType() {
                distance_type_id = 842, distance_type = "2m3f92y"
                },
                new DistanceType() {
                distance_type_id = 843, distance_type = "2m114y"
                },
                new DistanceType() {
                distance_type_id = 844, distance_type = "2m2f86y"
                },
                new DistanceType() {
                distance_type_id = 845, distance_type = "2m74y"
                },
                new DistanceType() {
                distance_type_id = 846, distance_type = "2m4f84y"
                },
                new DistanceType() {
                distance_type_id = 847, distance_type = "2m7f66y"
                },
                new DistanceType() {
                distance_type_id = 848, distance_type = "2m55y"
                },
                new DistanceType() {
                distance_type_id = 849, distance_type = "2m7f55y"
                },
                new DistanceType() {
                distance_type_id = 850, distance_type = "3m3f"
                },
                new DistanceType() {
                distance_type_id = 851, distance_type = "2m3f179y"
                },
                new DistanceType() {
                distance_type_id = 852, distance_type = "2m3f60y"
                },
                new DistanceType() {
                distance_type_id = 853, distance_type = "2m61y"
                },
                new DistanceType() {
                distance_type_id = 854, distance_type = "2m4f96y"
                },
                new DistanceType() {
                distance_type_id = 855, distance_type = "3m180y"
                },
                new DistanceType() {
                distance_type_id = 856, distance_type = "2m7f13y"
                },
                new DistanceType() {
                distance_type_id = 857, distance_type = "2m57y"
                },
                new DistanceType() {
                distance_type_id = 858, distance_type = "2m5f40y"
                },
                new DistanceType() {
                distance_type_id = 859, distance_type = "2m165y"
                },
                new DistanceType() {
                distance_type_id = 860, distance_type = "3m59y"
                },
                new DistanceType() {
                distance_type_id = 861, distance_type = "2m1f166y"
                },
                new DistanceType() {
                distance_type_id = 862, distance_type = "1m7f111y"
                },
                new DistanceType() {
                distance_type_id = 863, distance_type = "2m2f68y"
                },
                new DistanceType() {
                distance_type_id = 864, distance_type = "2m4f13y"
                },
                new DistanceType() {
                distance_type_id = 865, distance_type = "2m3f212y"
                },
                new DistanceType() {
                distance_type_id = 866, distance_type = "1m7f194y"
                },
                new DistanceType() {
                distance_type_id = 867, distance_type = "2m5f121y"
                },
                new DistanceType() {
                distance_type_id = 868, distance_type = "2m6f150y"
                },
                new DistanceType() {
                distance_type_id = 869, distance_type = "2m4f78y"
                },
                new DistanceType() {
                distance_type_id = 870, distance_type = "2m5f162y"
                },
                new DistanceType() {
                distance_type_id = 871, distance_type = "1m7f112y"
                },
                new DistanceType() {
                distance_type_id = 872, distance_type = "2m5f134y"
                },
                new DistanceType() {
                distance_type_id = 873, distance_type = "2m4f93y"
                },
                new DistanceType() {
                distance_type_id = 874, distance_type = "1m7f151y"
                },
                new DistanceType() {
                distance_type_id = 875, distance_type = "1m 4f"
                },
                new DistanceType() {
                distance_type_id = 876, distance_type = "1m 2½f"
                },
                new DistanceType() {
                distance_type_id = 877, distance_type = "2m 2½f"
                },
                new DistanceType() {
                distance_type_id = 878, distance_type = "2m ½f"
                },
                new DistanceType() {
                distance_type_id = 879, distance_type = "2m 3f"
                },
                new DistanceType() {
                distance_type_id = 880, distance_type = "2m 5½f"
                },
                new DistanceType() {
                distance_type_id = 881, distance_type = "2m 7f"
                },
                new DistanceType() {
                distance_type_id = 882, distance_type = "3m ½f"
                },
                new DistanceType() {
                distance_type_id = 883, distance_type = "1m 7½f"
                },
                new DistanceType() {
                distance_type_id = 884, distance_type = "2m 6½f"
                },
                new DistanceType() {
                distance_type_id = 885, distance_type = "2m 4½f"
                },
                new DistanceType() {
                distance_type_id = 886, distance_type = "2m 4f"
                },
                new DistanceType() {
                distance_type_id = 887, distance_type = "1m 7f"
                },
                new DistanceType() {
                distance_type_id = 888, distance_type = "3m 1f"
                },
                new DistanceType() {
                distance_type_id = 889, distance_type = "2m 7½f"
                },
                new DistanceType() {
                distance_type_id = 890, distance_type = "2m 5f"
                },
                new DistanceType() {
                distance_type_id = 891, distance_type = "1m 2f"
                },
                new DistanceType() {
                distance_type_id = 892, distance_type = "2m 3½f"
                },
                new DistanceType() {
                distance_type_id = 893, distance_type = "3m 2f"
                },
                new DistanceType() {
                distance_type_id = 894, distance_type = "3m 1½f"
                },
                new DistanceType() {
                distance_type_id = 895, distance_type = "1m 4½f"
                },
                new DistanceType() {
                distance_type_id = 896, distance_type = "2m 6f"
                },
                new DistanceType() {
                distance_type_id = 897, distance_type = "2m 1f"
                },
                new DistanceType() {
                distance_type_id = 898, distance_type = "3m 7½f"
                },
                new DistanceType() {
                distance_type_id = 899, distance_type = "1m 5f"
                },
                new DistanceType() {
                distance_type_id = 900, distance_type = "1m 3f"
                },
                new DistanceType() {
                distance_type_id = 901, distance_type = "2m 1½f"
                },
                new DistanceType() {
                distance_type_id = 902, distance_type = "1m ½f"
                },
                new DistanceType() {
                distance_type_id = 903, distance_type = "1m 1½f"
                },
                new DistanceType() {
                distance_type_id = 904, distance_type = "2m 2f"
                },
                new DistanceType() {
                distance_type_id = 905, distance_type = "1m 6f"
                },
                new DistanceType() {
                distance_type_id = 906, distance_type = "3m 5½f"
                },
                new DistanceType() {
                distance_type_id = 907, distance_type = "3m 5f"
                },
                new DistanceType() {
                distance_type_id = 908, distance_type = "3m 4½f"
                },
                new DistanceType() {
                distance_type_id = 909, distance_type = "3m 4f"
                },
                new DistanceType() {
                distance_type_id = 910, distance_type = "3m 3f"
                },
                new DistanceType() {
                distance_type_id = 911, distance_type = "3m 6½f"
                },
                new DistanceType() {
                distance_type_id = 912, distance_type = "4m 1½f"
                },
                new DistanceType() {
                distance_type_id = 913, distance_type = "1m 5½f"
                }
            };
        }
    }
}
