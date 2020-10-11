module.exports = {
  theme: {
    maxHeight: {
      '1/2': '50vh',
    },
    extend: {
      alphaColors: ['gray.700']
    },
  },
  variants: {},
  plugins: [
    require('tailwindcss-bg-alpha')(),
  ]
}
