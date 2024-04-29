import React from "react";
import "./FileUpload.css";
import Button from "../Button/Button";

interface FileUploadProps {
  onFileChange?: (event: React.ChangeEvent<HTMLInputElement>) => void;
  onUpload?: () => void;
  acceptedExtensions?: string;
  className?: string;
  buttonDisabled?: boolean;
  allowMultiple?: boolean;
}

const FileUpload: React.FC<FileUploadProps> = ({
  onFileChange,
  onUpload,
  acceptedExtensions,
  className = "",
  buttonDisabled,
  allowMultiple,
}) => {
  return (
    <div className={`FileUpload ${className}`}>
      <input
        type="file"
        accept={acceptedExtensions}
        onChange={onFileChange}
        multiple={allowMultiple}
      />
      <Button onClick={onUpload} disabled={buttonDisabled}>
        Upload
      </Button>
    </div>
  );
};

export default FileUpload;
