/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ["./src/**/*.{html,ts}"],
  theme: {
    extend: {},
  },
  plugins: [require("daisyui")],
  daisyui: {
    themes: [
      "light",
      "dark",
      "cupcake",
      {
        mytheme: {
          primary: "#bc00ff",

          secondary: "#00b300",

          accent: "#a52b00",

          neutral: "#342038",

          "base-100": "#fff5e7",

          info: "#00d1ff",

          success: "#82be00",

          warning: "#db2d00",

          error: "#ff3249",
        },
      },
    ],
  },
};
