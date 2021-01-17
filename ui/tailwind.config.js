module.exports = {
  theme: {
    maxHeight: {
      '1/2': '50vh',
    },
    extend: {
      fontFamily: {
        'sans': ['Open Sans', 'Helvetica', 'Arial', 'sans-serif']
      },
      alphaColors: ['gray.700']
    },
  },
  variants: {},
  plugins: [
    require('tailwindcss-bg-alpha')(),
  ]
}
