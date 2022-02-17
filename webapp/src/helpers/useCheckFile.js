import { ref } from "vue";

export const useCheckFile = () => {

  const check = ref(true);
  const validateMessage = ref("");

  const fileChecker = {
    // types: [extension, size] default ~20mb
    images: [/.(jpg|jpeg|png|jfif|gif)$/, 20 * 1024 * 1024],
  };

  function checkFile(file, fileType, fileSize = 0) {
    fileSize = fileSize === 0 ? fileChecker[fileType][1] : fileSize;
    if (file) {
      if (
        !file.name.match(fileChecker[fileType][0]) ||
        file.size > fileSize
      ) {
        var sizeError =
          file.size >= fileSize ? "File is too large, " : "";
        validateMessage.value = sizeError + `File must be ${fileType}, Please upload appropriate file!`;
        check.value = false;
        return false;
      } else {
        check.value = true;
        validateMessage.value = "";
        return true;
      }
    }
  }

  // check (boolean), validateMessage (string), checkfile (function)
  return { check, validateMessage, checkFile };
}