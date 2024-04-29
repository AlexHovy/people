export enum Gender {
  Other = 0,
  Male = 1,
  Female = 2,
}

export const genderDisplayNames: Map<Gender, string> = new Map([
  [Gender.Other, "Other"],
  [Gender.Male, "Male"],
  [Gender.Female, "Female"],
]);

export function getGenderDisplayName(gender: Gender): string {
  const displayName = genderDisplayNames.get(gender);
  return displayName || "Unknown";
}
