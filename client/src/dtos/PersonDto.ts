import { Gender } from "../constants/Gender";
import { BaseDto } from "./bases/BaseDto";

export interface PersonDto extends BaseDto {
  name: string;
  surname: string;
  gender: Gender;
  email: string;
  mobileNumber: string;
  addressCity: string;
  addressCountry: string;
  profilePicture: string;
}
