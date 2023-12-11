import { BaseDto } from "./bases/BaseDto";

export interface PersonDto extends BaseDto {
  parentCategoryId?: string;
  name: string;
  description?: string;
}
