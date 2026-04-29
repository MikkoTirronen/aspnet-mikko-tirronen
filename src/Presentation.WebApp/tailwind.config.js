export default {
  content: [
    "./Views/**/*.cshtml",
    "./Areas/**/*.cshtml",
    "./Pages/**/*.cshtml",
  ],
  theme: {
    extend: {
      colors: {
        theme: "var(--theme-color)",
        dark: "var(--dark-color)",
        gray: "var(--gray-color)",
        danger: "var(--danger-color)",
        text: "var(--text-color)",
        heading: "var(--heading-color)",
        background: "var(--body-background-color)",
      },
      fontFamily: {
        default: "var(--font-default)",
        heading: "var(--font-heading)",
      },
    },
  },
};
