import "./Dropdown.css";

interface DropdownProps<T> {
  name: string;
  options: { key: T; value: string }[];
  selectedValue: T;
  onChange: (value: T) => void;
  className?: string;
}

const Dropdown = <T extends number | string>({
  name,
  options,
  selectedValue,
  onChange,
  className = "",
}: DropdownProps<T>) => {
  return (
    <select
      name={name}
      value={selectedValue.toString()}
      onChange={(e) => onChange(e.target.value as unknown as T)}
      className={`dropdown ${className}`}
    >
      {options.map((option, index) => (
        <option key={index} value={option.key}>
          {option.value}
        </option>
      ))}
    </select>
  );
};

export default Dropdown;
