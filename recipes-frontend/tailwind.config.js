/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./src/**/*.{html,ts}"
  ],
  theme: {
    extend: {
      colors: {
        headerPrimary: '#212529',
        textPrimary: '#FFFFFF',
        textInverted: '#212529',
        textHovered: '#CBD5E1',
        textHoveredInverted: '#FFFFFF',
        googleRed: '#DB4437',

      }
    },
  },
  plugins: [],
}

