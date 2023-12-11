import { SettingNames } from "../constants/SettingNames";

export class SettingsConfig {
  static getBaseApiUrl(): string {
    return this.getEnvironmentVariable(SettingNames.BASE_API_URL);
  }
  
  private static getEnvironmentVariable(key: string): string {
    const value = process.env[key];
    if (!value) {
      throw new Error(`Environment variable ${key} is not set.`);
    }

    return value;
  }
}
