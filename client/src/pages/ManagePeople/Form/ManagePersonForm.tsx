import React, { useState, useEffect } from "react";
import "./ManagePersonForm.css";
import { PersonDto } from "../../../dtos/PersonDto";
import Input from "../../../components/Input/Input";
import Button from "../../../components/Button/Button";
import { Gender, getGenderDisplayName } from "../../../constants/Gender";
import Dropdown from "../../../components/Dropdown/Dropdown";
import { CountryService } from "../../../services/CountryService";
import { CityService } from "../../../services/CityService";
import { CountryDto } from "../../../dtos/CountryDto";
import { CityDto } from "../../../dtos/CityDto";
import { FileDto } from "../../../dtos/FileDto";
import { PersonService } from "../../../services/PersonService";
import FileUpload from "../../../components/FileUpload/FileUpload";
import { RegexPatterns } from "../../../constants/RegexPatterns";

interface ManagePersonFormProps {
  initialValues?: PersonDto | undefined;
  onSubmit: (person: PersonDto) => void;
}

const ManagePersonForm: React.FC<ManagePersonFormProps> = ({
  initialValues,
  onSubmit,
}) => {
  const personService = new PersonService();
  const countryService = new CountryService();
  const cityService = new CityService();

  const genderOptions = [
    { key: Gender.Other, value: getGenderDisplayName(Gender.Other) },
    { key: Gender.Male, value: getGenderDisplayName(Gender.Male) },
    { key: Gender.Female, value: getGenderDisplayName(Gender.Female) },
  ];

  const [isNewPerson] = useState<boolean>(initialValues === undefined);
  const [selectedFile, setSelectedFile] = useState<string | null>(null);

  const defaultPerson = {
    name: "",
    surname: "",
    gender: Gender.Other,
    email: "",
    mobileNumber: "",
    countryId: "",
    cityId: "",
    hasProfilePicture: false,
  } as PersonDto;
  const [person, setPerson] = useState<PersonDto>(defaultPerson);

  const [selectedGender, setSelectedGender] = useState<Gender>(person.gender);

  const [countries, setCountries] = useState<CountryDto[]>([]);
  const [selectedCountry, setSelectedCountry] = useState<CountryDto>();
  const [countryOptions, setCountryOptions] = useState<
    { key: any; value: string }[]
  >([{ key: "", value: "Select a country" }]);

  const [cities, setCities] = useState<CityDto[]>([]);
  const [selectedCity, setSelectedCity] = useState<CityDto>();
  const [cityOptions, setCityOptions] = useState<{ key: any; value: string }[]>(
    [{ key: "", value: "Select a city" }]
  );

  useEffect(() => {
    loadCountries();
    setPerson(initialValues || defaultPerson);
    setSelectedGender(initialValues?.gender || defaultPerson.gender);
  }, [initialValues]);

  const loadCountries = async () => {
    await countryService.get().then((countries) => {
      setCountries(countries);
      loadCountryOptions(countries);

      if (initialValues && initialValues.countryId !== "") {
        const country = countries.find((x) => x.id === initialValues.countryId);
        setSelectedCountry(country);
        loadCities(initialValues.countryId);
      }
    });
  };

  const loadCountryOptions = async (countries: CountryDto[]) => {
    const options: { key: any; value: string }[] = [
      { key: "", value: "Select a country" },
      ...countries.map((country) => ({
        key: country.id,
        value: country.name,
      })),
    ];
    setCountryOptions(options);
  };

  const loadCities = async (countryId: string) => {
    await cityService.getByCountryId(countryId).then((cities) => {
      setCities(cities);
      loadCityOptions(cities);

      if (initialValues && initialValues.cityId !== "") {
        const city = cities.find((x) => x.id === initialValues.cityId);
        setSelectedCity(city);
      }
    });
  };

  const loadCityOptions = async (cities: CityDto[]) => {
    const options: { key: any; value: string }[] = [
      { key: "", value: "Select a city" },
      ...cities.map((city) => ({
        key: city.id,
        value: city.name,
      })),
    ];
    setCityOptions(options);
  };

  const handleCountryChange = (countryId: string) => {
    if (!countryId || countryId === "") return;

    loadCities(countryId);

    const findCountry = countries.find((x) => x.id === countryId);
    setSelectedCountry(findCountry);

    person.countryId = countryId;
    person.country = findCountry?.name;
    setPerson(person);
  };

  const handleCityChange = (cityId: string) => {
    if (!cityId || cityId === "") return;

    const findCity = cities.find((x) => x.id === cityId);
    setSelectedCity(findCity);

    person.cityId = cityId;
    person.city = findCity?.name;
    setPerson(person);
  };

  const handleChange = (
    e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>
  ) => {
    setPerson({ ...person, [e.target.name]: e.target.value });
  };

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    if (person) onSubmit(person);
  };

  const handleGenderChange = (gender: any) => {
    person.gender = parseInt(gender);
    setPerson(person);
    setSelectedGender(person.gender);
  };

  const handleFileChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    const file = event.target.files?.[0];
    if (!file) return;

    if (file.type !== "image/png") return;

    const reader = new FileReader();
    reader.onloadend = () => {
      const result = reader.result as string;
      const base64 = extractBase64FromDataUrl(result);
      if (base64 || base64 !== "") setSelectedFile(base64);
    };
    reader.readAsDataURL(file);
  };

  const handleUpload = async () => {
    if (selectedFile) {
      const fileDto: FileDto = {
        fileName: `${person.id}.png`,
        fileBase64: selectedFile,
      };
      await personService.uploadProfilePicture(person.id, fileDto).then(() => {
        setSelectedFile(null);
      });
    }
  };

  const extractBase64FromDataUrl = (dataUrl: string): string => {
    const base64Match = dataUrl.match(RegexPatterns.BASE64_IMAGE_PNG);
    if (base64Match) {
      return base64Match[1];
    }
    return "";
  };

  return (
    <form onSubmit={handleSubmit}>
      <Input
        name="name"
        value={person.name}
        onChange={handleChange}
        placeholder="Name"
        required
      />
      <Input
        name="surname"
        value={person.surname}
        onChange={handleChange}
        placeholder="Surname"
        required
      />
      <Dropdown
        name="gender"
        options={genderOptions}
        selectedValue={selectedGender}
        onChange={handleGenderChange}
        required
      />
      <Input
        name="email"
        type="email"
        value={person.email}
        onChange={handleChange}
        placeholder="Email"
        required
      />
      <Input
        name="mobileNumber"
        value={person.mobileNumber}
        onChange={handleChange}
        placeholder="Mobile Number"
        required
      />
      <Dropdown
        name="country"
        options={countryOptions}
        selectedValue={selectedCountry?.id}
        onChange={handleCountryChange}
        required
      />
      <Dropdown
        name="city"
        options={cityOptions}
        selectedValue={selectedCity?.id}
        onChange={handleCityChange}
        required
      />
      {!isNewPerson && (
        <FileUpload
          acceptedExtensions=".png"
          onFileChange={handleFileChange}
          onUpload={handleUpload}
          buttonDisabled={!selectedFile}
        ></FileUpload>
      )}
      <Button type="submit">Save</Button>
    </form>
  );
};

export default ManagePersonForm;
